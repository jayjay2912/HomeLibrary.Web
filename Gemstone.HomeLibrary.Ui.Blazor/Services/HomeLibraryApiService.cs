using System.Net;
using System.Net.Http.Json;
using Gemstone.HomeLibrary.Shared.Models.HomeLibrary;

namespace Gemstone.HomeLibrary.Ui.Blazor.Services;

public class HomeLibraryApiService(IConfiguration configuration) : IHomeLibraryApiService
{
    public async Task<IEnumerable<Book>> GetLibrary()
    {
        var baseUrl = configuration.GetValue<string>("apiUrl");
        var url = $"{baseUrl}/library";

        using var client = new HttpClient();

        var result = await client.GetFromJsonAsync<IEnumerable<Book>>(url);

        if (result == null)
        {
            return new List<Book>();
        }

        return result;
    }

    public async Task<Book> GetBook(Guid bookId)
    {
        var baseUrl = configuration.GetValue<string>("apiUrl");
        var url = $"{baseUrl}/library/books/{bookId.ToString()}";

        using var client = new HttpClient();

        var result = await client.GetFromJsonAsync<Book>(url);

        if (result == null)
        {
            return new Book();
        }

        return result;
    }

    public async Task<bool> AddBookByIsbn(string isbn)
    {
        var baseUrl = configuration.GetValue<string>("apiUrl");
        var url = $"{baseUrl}/library/books/isbn/{isbn}";

        using var client = new HttpClient();

        var result = await client.PostAsync(url, null);

        if (result.StatusCode != HttpStatusCode.OK)
        {
            return false;
        }

        return true;
    }
}
