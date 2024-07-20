using Gemstone.HomeLibrary.Shared.Models.HomeLibrary;

namespace Gemstone.HomeLibrary.Api.Services.OpenLibraryService;

public interface IOpenLibraryService
{
    /// <inheritdoc cref="OpenLibraryService.TryGetBookByIsbn" />
    public Task<Book?> TryGetBookByIsbn(string isbn);
}
