using Gemstone.HomeLibrary.Api.Models.HomeLibrary;

namespace Gemstone.HomeLibrary.Api.Services.HomeLibraryService;

public interface IHomeLibraryService
{
    /// <inheritdoc cref="HomeLibraryService.GetLibrary" />
    public Task<List<Book>> GetLibrary();

    /// <inheritdoc cref="HomeLibraryService.GetBookById" />
    public Task<Book> GetBookById(Guid bookId);

    /// <inheritdoc cref="HomeLibraryService.TryGetBookByIsbn" />
    public Task<Book?> TryGetBookByIsbn(string isbn);

    /// <inheritdoc cref="HomeLibraryService.GetUserById" />
    public Task<User> GetUserById(Guid userId);

    /// <inheritdoc cref="HomeLibraryService.AddBook" />
    public Task AddBook(Book book);

    /// <inheritdoc cref="HomeLibraryService.UpdateBook" />
    public Task UpdateBook(Book book);

    /// <inheritdoc cref="HomeLibraryService.RecordBookReading" />
    public Task RecordBookReading(Book book, User user);

    /// <inheritdoc cref="HomeLibraryService.GetUnreadBooksForUser" />
    public Task<List<Book>> GetUnreadBooksForUser(User user);
}
