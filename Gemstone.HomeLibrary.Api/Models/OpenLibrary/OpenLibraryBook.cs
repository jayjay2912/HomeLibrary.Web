using System.Text.Json.Serialization;

namespace Gemstone.HomeLibrary.Api.Models.OpenLibrary;

/// <summary>
///     An <see cref="OpenLibraryBook" /> record as reported on the
///     <see cref="Gemstone.HomeLibrary.Api.Services.OpenLibraryService.OpenLibraryService" />
/// </summary>
/// <remarks>Transformed in to a standard <see cref="Models.HomeLibrary.Book" /> record before use</remarks>
public class OpenLibraryBook
{
    /// <summary>
    ///     The title of the book
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    /// <summary>
    ///     The subtitle of the book
    /// </summary>
    /// <remarks>Book may sometimes have an additional title</remarks>
    [JsonPropertyName("subtitle")]
    public string? SubTitle { get; set; }

    /// <summary>
    ///     The authors associated with a book
    /// </summary>
    /// <remarks>A book may be assigned multiple authors</remarks>
    [JsonPropertyName("authors")]
    public List<OpenLibraryAuthor>? Authors { get; set; }

    /// <summary>
    ///     The publishers associated with a book
    /// </summary>
    [JsonPropertyName("publishers")]
    public List<string>? Publishers { get; set; }

    /// <summary>
    ///     The ISBN-10 (International Standard Book Number) identifier
    /// </summary>
    [JsonPropertyName("isbn_10")]
    public List<string>? Isbn10 { get; set; }

    /// <summary>
    ///     The ISBN-13 (International Standard Book Number) identifier
    /// </summary>
    [JsonPropertyName("isbn_13")]
    public List<string>? Isbn13 { get; set; }

    /// <summary>
    ///     The number of pages in the book
    /// </summary>
    [JsonPropertyName("pagination")]
    public string? Pages { get; set; }

    /// <summary>
    ///     The date the book was published
    /// </summary>
    /// <remarks>Not always a full date, so use for display only</remarks>
    [JsonPropertyName("publish_date")]
    public string? PublishDate { get; set; }

    /// <summary>
    ///     The books key (URL) at the OpenLibrary API
    /// </summary>
    [JsonPropertyName("key")]
    public string? Key { get; set; }
}
