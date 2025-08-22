namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

/// <summary>
/// Request model for listing sales with pagination and filtering
/// </summary>
public class ListSalesRequest
{
    /// <summary>
    /// Page number (1-based)
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Number of items per page
    /// </summary>
    public int Size { get; set; } = 10;

    /// <summary>
    /// Filter by customer ID (optional)
    /// </summary>
    public Guid? CustomerId { get; set; }

    /// <summary>
    /// Filter by subsidiary ID (optional)
    /// </summary>
    public Guid? SubsidiaryId { get; set; }

    /// <summary>
    /// Filter by sale status (optional)
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Filter by start date (optional)
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Filter by end date (optional)
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Sort field (SaleDate, SaleNumber, TotalAmount, Status, CreatedAt)
    /// </summary>
    public string SortBy { get; set; } = "CreatedAt";

    /// <summary>
    /// Sort direction (asc, desc)
    /// </summary>
    public string SortDirection { get; set; } = "desc";
}