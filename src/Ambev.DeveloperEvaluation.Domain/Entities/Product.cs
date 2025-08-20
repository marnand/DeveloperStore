using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a product entity in the system
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Represents a product entity in the system
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the description of the product
    /// </summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the unit price of the product
    /// </summary>
    public decimal UnitPrice { get; set; }
    /// <summary>
    /// Gets or sets the stock quantity of the product
    /// </summary>
    public string Sku { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the date and time when the product was created
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Gets or sets the date and time when the product was last updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the Product class
    /// </summary>
    public Product()
    {
        CreatedAt = DateTime.UtcNow;
    }
}
