using System.ComponentModel.DataAnnotations.Schema;

namespace Gemstone.HomeLibrary.Api.Models.HomeLibrary;

/// <summary>
///     Tracks when a <see cref="Book" /> is read by <see cref="User" />
/// </summary>
public class ReadBook
{
    /// <summary>
    ///     The id
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    ///     The <see cref="User" /> that read it
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    ///     The <see cref="Book" /> that was read
    /// </summary>
    public Book Book { get; set; } = null!;

    /// <summary>
    ///     When the book was read
    /// </summary>
    public DateTime ReadAt { get; set; }
}
