namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Response model for UpdateSale operation
/// </summary>
public class UpdateSaleResult
{
    /// <summary>
    /// The unique identifier of the updated sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The sale number
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// The date when the sale was made
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// The customer identifier
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// The subsidiary identifier
    /// </summary>
    public Guid SubsidiaryId { get; set; }

    /// <summary>
    /// The current status of the sale
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// The total amount of the sale
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// The list of items in the sale
    /// </summary>
    public List<UpdateSaleItemResult> Items { get; set; } = [];

    /// <summary>
    /// When the sale was last updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Response model for sale item in UpdateSale operation
/// </summary>
public class UpdateSaleItemResult
{
    /// <summary>
    /// The unique identifier of the sale item
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The product identifier
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// The quantity of the product
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The unit price of the product
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// The discount percentage applied
    /// </summary>
    public decimal DiscountPercentage { get; set; }

    /// <summary>
    /// The total amount for this item
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Whether the item is cancelled
    /// </summary>
    public bool IsCancelled { get; set; }
}