namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Request model for updating an existing sale
/// </summary>
public class UpdateSaleRequest
{
    /// <summary>
    /// The unique identifier of the sale to update
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique identifier of the customer
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// The unique identifier of the subsidiary
    /// </summary>
    public Guid SubsidiaryId { get; set; }

    /// <summary>
    /// List of items to be updated in the sale
    /// </summary>
    public List<UpdateSaleItemRequest> Items { get; set; } = new();
}

/// <summary>
/// Request model for updating sale items
/// </summary>
public class UpdateSaleItemRequest
{
    /// <summary>
    /// The unique identifier of the sale item (null for new items)
    /// </summary>
    public Guid? Id { get; set; }

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