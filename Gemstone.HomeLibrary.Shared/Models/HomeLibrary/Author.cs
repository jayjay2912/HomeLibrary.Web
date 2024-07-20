using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gemstone.HomeLibrary.Shared.Models.HomeLibrary;

/// <summary>
///     The <see cref="Author" /> of a book
/// </summary>
/// <remarks>There may be more than one <see cref="Author" /> against a <see cref="Book" /> record</remarks>
public class Author
{
    /// <summary>
    ///     The id of the author
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    /// <summary>
    ///     The authors name
    /// </summary>
    /// <remarks>The authors name the related book was publisher under</remarks>
    [MaxLength(128)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     The authors personal name
    /// </summary>
    [MaxLength(128)]
    public string PersonalName { get; set; } = string.Empty;

    /// <summary>
    ///     The authors key (URL) at the OpenLibrary API
    /// </summary>
    [MaxLength(128)]
    public string? OpenLibraryKey { get; set; }
}
