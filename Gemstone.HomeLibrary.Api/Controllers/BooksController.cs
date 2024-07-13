using Gemstone.HomeLibrary.Api.Models;
using Gemstone.HomeLibrary.Api.Models.HomeLibrary;
using Gemstone.HomeLibrary.Api.Services.HomeLibraryService;
using Gemstone.HomeLibrary.Api.Services.OpenLibraryService;
using Microsoft.AspNetCore.Mvc;

namespace Gemstone.HomeLibrary.Api.Controllers;

[ApiController]
[Route("books")]
public class BooksController(IServiceProvider serviceProvider) : ControllerBase
{
    private readonly IHomeLibraryService
        _homeLibraryService = serviceProvider.GetRequiredService<IHomeLibraryService>();

    private readonly IOpenLibraryService
        _openLibraryService = serviceProvider.GetRequiredService<IOpenLibraryService>();

    /// <summary>
    ///     Fetch the library
    /// </summary>
    /// <returns>The list of <see cref="Book" /> records in the library</returns>
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetLibrary()
    {
        var books = await _homeLibraryService.GetLibrary();
        return Ok(books);
    }

    /// <summary>
    ///     Find a <see cref="Book" /> by ISIN and add it to the library
    /// </summary>
    /// <param name="isbn">The 10 or 13 character ISBN</param>
    /// <returns>Ok if added, Not Found if the ISBN could not be found</returns>
    [HttpPost]
    [Route("{isbn}")]
    public async Task<IActionResult> AddBookByIsin([FromRoute] string isbn)
    {
        var existingBook = await _homeLibraryService.TryGetBookByIsbn(isbn);
        if (existingBook != null)
            return Conflict(new GenericResponse
            {
                Message = $"ISBN {isbn} already exists with against book {existingBook.Id}"
            });

        var book = await _openLibraryService.TryGetBookByIsbn(isbn);
        if (book == null)
            return NotFound(new GenericResponse
            {
                Message = $"Could not find book with ISBN: {isbn}"
            });

        await _homeLibraryService.AddBook(book);

        return Ok(new GenericResponse
        {
            Message = $"Book added under id {book.Id}"
        });
    }

    /// <summary>
    ///     Add a <see cref="Book" /> manually if it can't be found by a lookup
    /// </summary>
    /// <returns>Ok if added, Conflict if the record already exists</returns>
    [HttpPost]
    [Route("manual")]
    public async Task<IActionResult> AddBookManually([FromBody] Book book)
    {
        var existingBook = await _homeLibraryService.TryGetBookByIsbn(book.Isbn10 ?? book.Isbn13);
        if (existingBook != null)
            return Conflict(new GenericResponse
            {
                Message = $"Book already exists as {existingBook.Id} with ISBN {book.Isbn10 ?? book.Isbn13}"
            });

        await _homeLibraryService.AddBook(book);

        return Ok(new GenericResponse
        {
            Message = $"Book added manually under id {book.Id}"
        });
    }

    /// <summary>
    ///     Record a <see cref="Book" /> as read by a <see cref="User" />
    /// </summary>
    /// <returns>Ok if saved</returns>
    [HttpPost]
    [Route("{bookId:guid}/read/{userId:guid}")]
    public async Task<IActionResult> MarkBookAsRead([FromRoute] Guid bookId, [FromRoute] Guid userId)
    {
        var book = await _homeLibraryService.GetBookById(bookId);
        var user = await _homeLibraryService.GetUserById(userId);

        await _homeLibraryService.RecordBookReading(book, user);

        return Ok(new GenericResponse
        {
            Message = $"Marked book {bookId} as read by {userId}"
        });
    }
}
