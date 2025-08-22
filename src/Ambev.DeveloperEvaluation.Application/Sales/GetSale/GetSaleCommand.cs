using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Command for get a sale by I.
/// </summary>
public class GetSaleCommand : IRequest<GetSaleResult>
{
    /// <summary>
    /// Gets or sets the unique identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Initializes a new instance of GetSaleCommand.
    /// </summary>
    /// <param name="id">The sale Ii</param>
    public GetSaleCommand(Guid id)
    {
        Id = id;
    }
}