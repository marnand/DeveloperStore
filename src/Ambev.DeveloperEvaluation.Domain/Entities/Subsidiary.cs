using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a subsidiary entity in the system
/// </summary>
public class Subsidiary : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the subsidiary
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the address of the subsidiary
    /// </summary>
    public string Address { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the contact phone number for the subsidiary
    /// </summary>
    public string Phone { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the date and time when the subsidiary was created
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Gets or sets the date and time when the subsidiary was last updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the Subsidiary.
    /// </summary>
    public Subsidiary()
    {
        CreatedAt = DateTime.UtcNow;
    }
}
