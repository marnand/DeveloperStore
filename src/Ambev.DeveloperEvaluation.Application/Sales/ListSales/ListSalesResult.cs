namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

/// <summary>
/// Response model for ListSales operation with pagination
/// </summary>
public class ListSalesResult
{
    /// <summary>
    /// The list of sales
    /// </summary>
    public List<SaleListItem> Sales { get; set; } = [];

    /// <summary>
    /// Current page number
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Total number of pages
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Page size
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total number of sales
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Indicates if there is a previous page
    /// </summary>
    public bool HasPrevious { get; set; }

    /// <summary>
    /// Indicates if there is a next page
    /// </summary>
    public bool HasNext { get; set; }
}

/// <summary>
/// Sale item for list operations
/// </summary>
public class SaleListItem
{
    /// <summary>
    /// The unique identifier of the sale
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
    /// The number of items in the sale
    /// </summary>
    public int ItemCount { get; set; }

    /// <summary>
    /// When the sale was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// When the sale was last updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}