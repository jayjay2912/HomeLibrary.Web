using System.Text.Json;
using Gemstone.HomeLibrary.Api.Models.OpenLibrary;
using Gemstone.HomeLibrary.Shared.Models.HomeLibrary;

namespace Gemstone.HomeLibrary.Api.Services.OpenLibraryService;

/// <summary>
///     Interact with the OpenLibrary API to fetch a <see cref="Book" /> and its related information
/// </summary>
/// <param name="serviceProvider">Service provider</param>
/// <param name="logger">Logging</param>
public class OpenLibraryService(IServiceProvider serviceProvider, ILogger<IOpenLibraryService> logger)
    : IOpenLibraryService
{
    private readonly IHttpClientFactory _httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

    /// <summary>
    ///     Fetch a book from the OpenLibrary API by its ISBN
    /// </summary>
    /// <param name="isbn">The books 10 or 13 character ISBN</param>
    /// <returns>The <see cref="Book" /> or null if the book cannot be found</returns>
    public async Task<Book?> TryGetBookByIsbn(string isbn)
    {
        using var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Add("User-Agent", GetUserAgent());

        logger.LogInformation("Fetching book by ISBN {Isbn}", isbn);

        var openLibraryBook = await client.GetFromJsonAsync<OpenLibraryBook>(
            $"https://openlibrary.org/isbn/{isbn}.json",
            new JsonSerializerOptions(JsonSerializerDefaults.Web));

        if (openLibraryBook == null)
        {
            logger.LogInformation("Could not find book on OpenLibrary");
            return null;
        }

        logger.LogInformation("Found book with key {Key}", openLibraryBook.Key);

        var book = new Book
        {
            Title = openLibraryBook.Title,
            SubTitle = openLibraryBook.SubTitle,
            Authors = await CleanAuthors(openLibraryBook),
            Publishers = CleanPublishers(openLibraryBook),
            Isbn10 = openLibraryBook.Isbn10?.FirstOrDefault(),
            Isbn13 = openLibraryBook.Isbn13?.FirstOrDefault(),
            Pages = openLibraryBook.Pages,
            PublishDate = openLibraryBook.PublishDate,
            OpenLibraryKey = openLibraryBook.Key
        };

        return book;
    }

    /// <summary>
    ///     User-Agent string to provide to the OpenLibrary API in all HTTP requests
    /// </summary>
    /// <remarks>As requested https://openlibrary.org/developers/api</remarks>
    /// <returns>A user agent string with identifying information</returns>
    private static string GetUserAgent()
    {
        return "HomeLibrary.Web/Jay Stevenson/jaystevenson2912_at_gmail.com";
    }

    /// <summary>
    ///     Processes a list of author <see cref="OpenLibraryAuthor.Key" /> records attached to a
    ///     <see cref="OpenLibraryBook" />  from the OpenLibrary API into into actual <see cref="Author" /> records
    /// </summary>
    /// <param name="openLibraryBook">The book to process</param>
    /// <returns>A list of <see cref="Author" /> records or an empty list if there are none</returns>
    private async Task<List<Author>> CleanAuthors(OpenLibraryBook openLibraryBook)
    {
        var authors = new List<Author>();
        if (openLibraryBook.Authors == null)
        {
            logger.LogInformation("Book has no authors");
            return authors;
        }

        var tasks = openLibraryBook.Authors.ToList().Select(i => GetAuthorByKey(i.Key));
        var results = await Task.WhenAll(tasks);

        foreach (var openLibraryAuthor in results)
        {
            authors.Add(new Author
            {
                Name = openLibraryAuthor?.Name ?? string.Empty,
                PersonalName = openLibraryAuthor?.PersonalName ?? string.Empty,
                OpenLibraryKey = openLibraryAuthor?.Key ?? string.Empty
            });
        }

        return authors;
    }

    /// <summary>
    ///     Processes a list of publishers attached to a <see cref="OpenLibraryBook" />  from the OpenLibrary API into actual
    ///     <see cref="Publisher" /> records
    /// </summary>
    /// <param name="openLibraryBook">The book to process</param>
    /// <returns>A list of <see cref="Publisher" /> records or an empty list if there are none</returns>
    private List<Publisher> CleanPublishers(OpenLibraryBook openLibraryBook)
    {
        var publishers = new List<Publisher>();
        if (openLibraryBook.Authors == null)
        {
            logger.LogInformation("Book has no publishers");
            return publishers;
        }

        openLibraryBook.Publishers?.ForEach(publisher =>
        {
            publishers.Add(new Publisher
            {
                Name = publisher
            });
        });
        return publishers;
    }

    /// <summary>
    ///     Fetch an author from the OpenLibrary API by their author key
    /// </summary>
    /// <param name="authorKey">The author key (URL)</param>
    /// <returns>The <see cref="OpenLibraryAuthor" /> or null if the author cannot be found</returns>
    private async Task<OpenLibraryAuthor?> GetAuthorByKey(string authorKey)
    {
        using var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Add("User-Agent", GetUserAgent());

        logger.LogInformation("Fetching author by key {AuthorKey}", authorKey);

        var openLibraryAuthor = await client.GetFromJsonAsync<OpenLibraryAuthor>(
            $"https://openlibrary.org/{authorKey}.json",
            new JsonSerializerOptions(JsonSerializerDefaults.Web));

        return openLibraryAuthor;
    }
}
