using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Command for updating an existing sale.
/// </summary>
public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    /// <summary>
    /// Gets or sets the ID of the sale to update.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the customerId for sale.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the subsidiaryId where sale.
    /// </summary>
    public Guid SubsidiaryId { get; set; }

    /// <summary>
    /// Gets or sets the list of items in sale.
    /// </summary>
    public List<UpdateSaleItemCommand> Items { get; set; } = [];
}

/// <summary>
/// Command for updating a sale item.
/// </summary>
public class UpdateSaleItemCommand
{
    /// <summary>
    /// Gets or sets the ID of the item (optional for new items).
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Gets or sets the productId.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of product.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of product.
    /// </summary>
    public decimal UnitPrice { get; set; }
}