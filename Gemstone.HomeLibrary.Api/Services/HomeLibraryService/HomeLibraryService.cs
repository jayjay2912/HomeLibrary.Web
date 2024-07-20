using Gemstone.HomeLibrary.Api.DbContext;
using Gemstone.HomeLibrary.Shared.Models.HomeLibrary;
using Microsoft.EntityFrameworkCore;

namespace Gemstone.HomeLibrary.Api.Services.HomeLibraryService;

/// <summary>
///     Interact with the local library database to manage your library and reading lists
/// </summary>
/// <param name="serviceProvider">Service provider</param>
/// <param name="logger">Logging</param>
public class HomeLibraryService(IServiceProvider serviceProvider, ILogger<HomeLibraryService> logger)
    : IHomeLibraryService
{
    private readonly LibraryDbContext _db = serviceProvider.GetRequiredService<LibraryDbContext>();

    /// <summary>
    ///     Fetches all of a users <see cref="Book" /> records
    /// </summary>
    /// <returns>A list of <see cref="Book" /> records in the library</returns>
    public async Task<List<Book>> GetLibrary()
    {
        var books = await _db.Books
            .Include(b => b.Publishers)
            .Include(b => b.Authors)
            .ToListAsync();
        return books;
    }

    /// <summary>
    ///     Adds a <see cref="Book" /> record to the library
    /// </summary>
    /// <returns>None</returns>
    public async Task AddBook(Book book)
    {
        book.AddedAt = DateTime.UtcNow;
        _db.Books.Add(book);
        await _db.SaveChangesAsync();
        logger.LogInformation("Added new book {Id}", book.Id);
    }

    /// <summary>
    ///     Updates a <see cref="Book" /> record in the library
    /// </summary>
    /// <returns>None</returns>
    public async Task UpdateBook(Book book)
    {
        book.AddedAt = DateTime.UtcNow;
        _db.Books.Add(book);
        await _db.SaveChangesAsync();
        logger.LogInformation("Added new book {Id}", book.Id);
    }

    /// <summary>
    ///     Get a <see cref="Book" /> record by its <see cref="Book.Id" />
    /// </summary>
    /// <param name="bookId">The <see cref="Book.Id" /> of the <see cref="Book" /> to fetch</param>
    /// <returns>The <see cref="Book" /> or null</returns>
    public async Task<Book> GetBookById(Guid bookId)
    {
        var book = await _db.Books.Where(b => b.Id.Equals(bookId)).FirstOrDefaultAsync();
        if (book == null)
        {
            throw new ArgumentException($"Unknown book id {bookId}");
        }
        return book;
    }

    /// <summary>
    ///     Get a <see cref="User" /> record by their  <see cref="User.Id" />
    /// </summary>
    /// <param name="userId">The <see cref="User.Id" /> of the <see cref="User" /> to fetch</param>
    /// <returns>The <see cref="User" /> or null</returns>
    public async Task<User> GetUserById(Guid userId)
    {
        var user = await _db.Users.Where(u => u.Id.Equals(userId)).FirstOrDefaultAsync();
        if (user == null)
        {
            throw new ArgumentException($"Unknown user id {userId}");
        }
        return user;
    }

    /// <summary>
    ///     Searches the library for a <see cref="Book" /> by its ISBN
    /// </summary>
    /// <param name="isbn">The ISBN to search for</param>
    /// <returns>The first matching <see cref="Book" /> or null if one is not found</returns>
    public async Task<Book?> TryGetBookByIsbn(string isbn)
    {
        var existingBook = await _db.Books
            .Where(b => b.Isbn10 != null && b.Isbn10.Equals(isbn) || b.Isbn13 != null && b.Isbn13.Equals(isbn))
            .ToListAsync();

        return existingBook.FirstOrDefault();
    }

    /// <summary>
    ///     Records the reading of a <see cref="Book" /> by a <see cref="User" />
    /// </summary>
    /// <param name="book">The <see cref="Book" /> that was read</param>
    /// <param name="user">The <see cref="User" /> it was read by</param>
    /// <remarks>If the <see cref="Book" /> was already read, the read at time is updated</remarks>
    /// <returns>None</returns>
    public async Task RecordBookReading(Book book, User user)
    {
        var alreadyRead = await _db.ReadBooks
            .Where(r => r.User.Equals(user) && r.Book.Equals(book))
            .FirstOrDefaultAsync();
        if (alreadyRead != null)
        {
            alreadyRead.ReadAt = DateTime.UtcNow;
            _db.Update(alreadyRead);
            await _db.SaveChangesAsync();
            logger.LogInformation("Updated recent reading of book {BookId} as read by {UserId}", book.Id, user.Id);
            return;
        }

        _db.ReadBooks.Add(new ReadBook
        {
            User = user,
            Book = book,
            ReadAt = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();
        logger.LogInformation("Marked book {BookId} as read by {UserId}", book.Id, user.Id);
    }

    /// <summary>
    ///     Get <see cref="Book" /> records that are yet to be read by a <see cref="User" />
    /// </summary>
    /// <param name="user">The <see cref="User" /> we are finding unread books for</param>
    /// <returns>A list of <see cref="Book" /> records in the library that are unread or an empty list</returns>
    public async Task<List<Book>> GetUnreadBooksForUser(User user)
    {
        return [];

        // TODO
        // var sql = @"
        //     select
        //         b.Id,
        //         b.Title ,
        //         u.Id,
        //         u.Name ,
        //         rb.ReadAt
        //     from
        //         Books b
        //         left join Users u
        //         left join ReadBooks rb on b.Id = rb.BookId and u.Id = rb.UserId
        //     WHERE
        //         u.Id = 'A6AAA168-E033-4992-8B7C-317671599573'
        //         and rb.Id is null
        // ";
    }
}
