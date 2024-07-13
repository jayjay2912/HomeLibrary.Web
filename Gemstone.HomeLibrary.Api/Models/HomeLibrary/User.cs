using System.ComponentModel.DataAnnotations;

namespace Gemstone.HomeLibrary.Api.Models.HomeLibrary;

/// <summary>
///     A <see cref="User" />
/// </summary>
/// <remarks>It's probably you</remarks>
public class User
{
    /// <summary>
    ///     The id of the user
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     The name of the user
    /// </summary>
    [MaxLength(128)]
    public string Name { get; set; } = null!;
}
