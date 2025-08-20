using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a customer entity in the system
/// </summary>
public class Customer : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the customer
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the email address of the customer
    /// </summary>
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the phone number of the customer
    /// </summary>
    public string Phone { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the address of the customer
    /// </summary>
    public string Document { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the date and time when the customer was created
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Gets or sets the date and time when the customer was last updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    public Customer()
    {
        CreatedAt = DateTime.UtcNow;
    }
}
