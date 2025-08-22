using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Response model for CreateSale operation
/// </summary>
public class CreateSaleResponse
{
    /// <summary>
    /// The unique identifier of the created sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The generated sale number
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// The date when the sale was created
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// The unique identifier of the customer
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// The unique identifier of the subsidiary
    /// </summary>
    public Guid SubsidiaryId { get; set; }

    /// <summary>
    /// The current status of the sale
    /// </summary>
    public SaleStatus Status { get; set; }

    /// <summary>
    /// The total amount of the sale
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// When the sale was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// List of items in the sale
    /// </summary>
    public List<CreateSaleItemResponse> Items { get; set; } = new();
}

/// <summary>
/// Response model for sale items
/// </summary>
public class CreateSaleItemResponse
{
    /// <summary>
    /// The unique identifier of the sale item
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique identifier of the product
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
    /// The discount percentage applied to this item
    /// </summary>
    public decimal DiscountPercentage { get; set; }

    /// <summary>
    /// The total amount for this item (quantity * unit price - discount)
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Indicates whether this item has been cancelled
    /// </summary>
    public bool IsCancelled { get; set; }
}