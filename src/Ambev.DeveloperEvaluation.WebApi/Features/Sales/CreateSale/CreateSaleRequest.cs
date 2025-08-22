namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Request model for creating a new sale
/// </summary>
public class CreateSaleRequest
{
    /// <summary>
    /// The unique identifier of the customer making the purchase
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// The unique identifier of the subsidiary where the sale is made
    /// </summary>
    public Guid SubsidiaryId { get; set; }

    /// <summary>
    /// List of items to be included in the sale
    /// </summary>
    public List<CreateSaleItemRequest> Items { get; set; } = new();
}

/// <summary>
/// Request model for sale items
/// </summary>
public class CreateSaleItemRequest
{
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
}