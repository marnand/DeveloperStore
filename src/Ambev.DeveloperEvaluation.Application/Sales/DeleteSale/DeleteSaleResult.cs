namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Response model for DeleteSale operation
/// </summary>
public class DeleteSaleResult
{
    /// <summary>
    /// The unique identifier of the deleted sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Indicates whether the deletion was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Message describing the result of the operation
    /// </summary>
    public string Message { get; set; } = string.Empty;
}