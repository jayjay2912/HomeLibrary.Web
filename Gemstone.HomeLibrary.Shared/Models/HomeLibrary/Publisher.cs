using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gemstone.HomeLibrary.Shared.Models.HomeLibrary;

/// <summary>
///     The <see cref="Publisher" /> of a book
/// </summary>
/// <remarks>There may be more than one <see cref="Publisher" /> against a <see cref="Book" /> record</remarks>
public class Publisher
{
    /// <summary>
    ///     The id of the publisher
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    /// <summary>
    ///     The publishers name
    /// </summary>
    [MaxLength(128)]
    public string Name { get; set; } = string.Empty;
}
