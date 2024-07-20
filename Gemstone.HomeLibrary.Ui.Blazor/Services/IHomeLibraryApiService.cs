using Gemstone.HomeLibrary.Shared.Models.HomeLibrary;

namespace Gemstone.HomeLibrary.Ui.Blazor.Services;

public interface IHomeLibraryApiService
{
    public Task<IEnumerable<Book>> GetLibrary();

    public Task<Book> GetBook(Guid bookId);

    public Task<bool> AddBookByIsbn(string isbn);
}
