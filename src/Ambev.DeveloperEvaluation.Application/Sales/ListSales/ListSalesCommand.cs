using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

/// <summary>
/// Command for listing sales with pagination and filtering options.
/// </summary>
public class ListSalesCommand : IRequest<ListSalesResult>
{
    /// <summary>
    /// Gets or sets the page number (1-based)
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the page size (number of items per page)
    /// </summary>
    public int Size { get; set; } = 10;

    /// <summary>
    /// Gets or sets the customer ID filter (optional)
    /// </summary>
    public Guid? CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the subsidiary ID filter (optional)
    /// </summary>
    public Guid? SubsidiaryId { get; set; }

    /// <summary>
    /// Gets or sets the sale status filter (optional)
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the start date filter (optional)
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date filter (optional)
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets the sort field (optional)
    /// </summary>
    public string? SortBy { get; set; }

    /// <summary>
    /// Gets or sets the sort direction (asc/desc)
    /// </summary>
    public string SortDirection { get; set; } = "desc";
}