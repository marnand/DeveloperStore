using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Command for creating a new sale.
/// </summary>
public class CreateSaleCommand : IRequest<CreateSaleResult>
{
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
    public List<CreateSaleItemCommand> Items { get; set; } = [];
}

/// <summary>
/// Command for creating a sale item.
/// </summary>
public class CreateSaleItemCommand
{
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