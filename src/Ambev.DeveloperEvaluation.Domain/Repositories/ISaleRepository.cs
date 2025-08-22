using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Sale
/// </summary>
public interface ISaleRepository
{
    /// <summary>
    /// Creates a new sale
    /// </summary>
    /// <param name="sale">Sale to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created sale</returns>
    Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a sale by unique identifier
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Sale if found, null </returns>
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a sale by sale number
    /// </summary>
    /// <param name="saleNumber">Sale number to search</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Sale if found, null</returns>
    Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default);

    /// <summary>
    /// get sales by customer ID
    /// </summary>
    /// <param name="customerId">Customer ID to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of sales for customer</returns>
    Task<IEnumerable<Sale>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get sales by subsidiaryId
    /// </summary>
    /// <param name="branchId">subsidiaryId to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of sales for subsidiary</returns>
    Task<IEnumerable<Sale>> GetByBranchIdAsync(Guid subsidiaryId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get sales by status
    /// </summary>
    /// <param name="status">Status to filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of sales with </returns>
    Task<IEnumerable<Sale>> GetByStatusAsync(SaleStatus status, CancellationToken cancellationToken = default);

    /// <summary>
    /// get sales within a date range
    /// </summary>
    /// <param name="startDate">Start date range</param>
    /// <param name="endDate">End date range</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of sales within the date range</returns>
    Task<IEnumerable<Sale>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all sales with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of sales</returns>
    Task<IEnumerable<Sale>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get paginated sales with filtering and sorting options
    /// </summary>
    /// <param name="page">Page number (1-based)</param>
    /// <param name="size">Page size</param>
    /// <param name="customerId">Optional customer ID filter</param>
    /// <param name="subsidiaryId">Optional subsidiary ID filter</param>
    /// <param name="status">Optional status filter</param>
    /// <param name="startDate">Optional start date filter</param>
    /// <param name="endDate">Optional end date filter</param>
    /// <param name="sortBy">Field to sort by</param>
    /// <param name="sortDirection">Sort direction (asc/desc)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Tuple containing the list of sales and total count</returns>
    Task<(IEnumerable<Sale> Sales, int TotalCount)> GetPaginatedAsync(
        int page,
        int size,
        Guid? customerId = null,
        Guid? subsidiaryId = null,
        SaleStatus? status = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        string sortBy = "SaleDate",
        string sortDirection = "desc",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates
    /// </summary>
    /// <param name="sale">Sale to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated sale</returns>
    Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a sale 
    /// </summary>
    /// <param name="id">unique identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the sale was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}