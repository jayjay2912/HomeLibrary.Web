using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gemstone.HomeLibrary.Shared.Models.HomeLibrary;

/// <summary>
///     A <see cref="Book" />. The reason we're here.
/// </summary>
/// <remarks>Reading Is Fun!</remarks>
public class Book
{
    /// <summary>
    ///     The id of the book
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    /// <summary>
    ///     The title of the book
    /// </summary>
    [MaxLength(128)]
    public string Title { get; set; } = null!;

    /// <summary>
    ///     The subtitle of the book
    /// </summary>
    /// <remarks>Book may sometimes have an additional title, the rest of the time it is null</remarks>
    [MaxLength(128)]
    public string? SubTitle { get; set; }

    /// <summary>
    ///     The authors associated with a book
    /// </summary>
    /// <remarks>A book may be assigned multiple authors</remarks>
    public List<Author>? Authors { get; set; }

    /// <summary>
    ///     The publishers associated with a book
    /// </summary>
    public List<Publisher>? Publishers { get; set; }

    /// <summary>
    ///     The ISBN-10 (International Standard Book Number) identifier
    /// </summary>
    [MaxLength(10)]
    public string? Isbn10 { get; set; }

    /// <summary>
    ///     The ISBN-13 (International Standard Book Number) identifier
    /// </summary>
    [MaxLength(13)]
    public string? Isbn13 { get; set; }

    /// <summary>
    ///     The number of pages in the book
    /// </summary>
    [MaxLength(12)]
    public string? Pages { get; set; }

    /// <summary>
    ///     The date the book was published
    /// </summary>
    /// <remarks>Not always a full date, use for display only</remarks>
    [MaxLength(32)]
    public string? PublishDate { get; set; }

    /// <summary>
    ///     The books key (URL) at the OpenLibrary API
    /// </summary>
    [MaxLength(128)]
    public string? OpenLibraryKey { get; set; }

    /// <summary>
    ///     When the book was added
    /// </summary>
    public DateTime AddedAt { get; set; }
}
