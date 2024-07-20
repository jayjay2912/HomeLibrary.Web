using System.Text.Json.Serialization;
using Gemstone.HomeLibrary.Shared.Models.HomeLibrary;

namespace Gemstone.HomeLibrary.Api.Models.OpenLibrary;

/// <summary>
///     An <see cref="OpenLibraryAuthor" /> record as reported on the
///     <see cref="Gemstone.HomeLibrary.Api.Services.OpenLibraryService.OpenLibraryService" />
/// </summary>
/// <remarks>Transformed in to a standard <see cref="Author" /> record before use</remarks>
public class OpenLibraryAuthor
{
    /// <summary>
    ///     The authors name
    /// </summary>
    /// <remarks>
    ///     This will be empty when attached to a <see cref="OpenLibraryBook" /> record and is only populated when
    ///     fetching an author directly
    /// </remarks>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     The authors personal name
    /// </summary>
    /// <remarks>
    ///     This will be empty when attached to a <see cref="OpenLibraryBook" /> record and is only populated when
    ///     fetching an author directly
    /// </remarks>
    [JsonPropertyName("personal_name")]
    public string PersonalName { get; set; } = string.Empty;

    /// <summary>
    ///     The authors key (URL) at the OpenLibrary API
    /// </summary>
    /// <remarks>
    ///     This is only the only property we receive when fetching an <see cref="OpenLibraryBook" />, otherwise we
    ///     receive all fields
    /// </remarks>
    [JsonPropertyName("key")]
    public string Key { get; set; } = null!;
}
