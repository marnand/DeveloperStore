namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Represents response returned after successfully creating a new sale.
/// </summary>
public class CreateSaleResult
{
    /// <summary>
    /// Gets or sets the unique identifier of new created sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the sale number.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total amount of sale.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the discount applied to sale.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets or sets the create date of sale.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}