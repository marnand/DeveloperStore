using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Response model for GetSale.
/// </summary>
public class GetSaleResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the sale number.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer Id.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the subsidiary Id .
    /// </summary>
    public Guid SubsidiaryId { get; set; }

    /// <summary>
    /// Gets or sets the sale status.
    /// </summary>
    public SaleStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the total amount.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the discount.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets or sets the sale items.
    /// </summary>
    public List<GetSaleItemResult> Items { get; set; } = [];

    /// <summary>
    /// Gets or sets the creation date.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last update date.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Response model for sale item.
/// </summary>
public class GetSaleItemResult
{
    /// <summary>
    /// Gets or sets the item Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the product Id.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets unit price.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the total price for this item.
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Gets or sets item is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}