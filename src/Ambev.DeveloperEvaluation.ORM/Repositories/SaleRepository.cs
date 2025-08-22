using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ISaleRepository using Entity Framework Core
/// </summary>
public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of SaleRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new sale in the database
    /// </summary>
    /// <param name="sale">The sale to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale</returns>
    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    /// <summary>
    /// Retrieves a sale by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, null otherwise</returns>
    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    /// <summary>
    /// Updates an existing sale in the database
    /// </summary>
    /// <param name="sale">The sale to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated sale</returns>
    public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        _context.Sales.Update(sale);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    /// <summary>
    /// Deletes a sale from the database
    /// </summary>
    /// <param name="id">The unique identifier of the sale to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the sale was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale = await GetByIdAsync(id, cancellationToken);
        if (sale == null)
            return false;

        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Get a sale by sale number
    /// </summary>
    /// <param name="saleNumber">Sale number to search</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Sale if found, null</returns>
    public async Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.SaleNumber == saleNumber, cancellationToken);
    }

    /// <summary>
    /// Get sales by customer ID
    /// </summary>
    /// <param name="customerId">Customer ID to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of sales for customer</returns>
    public async Task<IEnumerable<Sale>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .Where(s => s.CustomerId == customerId)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Get sales by subsidiary ID
    /// </summary>
    /// <param name="subsidiaryId">Subsidiary ID to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of sales for subsidiary</returns>
    public async Task<IEnumerable<Sale>> GetByBranchIdAsync(Guid subsidiaryId, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .Where(s => s.SubsidiaryId == subsidiaryId)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Get sales by status
    /// </summary>
    /// <param name="status">Status to filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of sales with specified status</returns>
    public async Task<IEnumerable<Sale>> GetByStatusAsync(SaleStatus status, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .Where(s => s.Status == status)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Get sales within a date range
    /// </summary>
    /// <param name="startDate">Start date range</param>
    /// <param name="endDate">End date range</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of sales within the date range</returns>
    public async Task<IEnumerable<Sale>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Get all sales with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of sales</returns>
    public async Task<IEnumerable<Sale>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

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
    public async Task<(IEnumerable<Sale> Sales, int TotalCount)> GetPaginatedAsync(
        int page,
        int size,
        Guid? customerId = null,
        Guid? subsidiaryId = null,
        SaleStatus? status = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        string sortBy = "SaleDate",
        string sortDirection = "desc",
        CancellationToken cancellationToken = default)
    {
        var query = _context.Sales.Include(s => s.Items).AsQueryable();

        // Apply filters
        if (customerId.HasValue)
            query = query.Where(s => s.CustomerId == customerId.Value);

        if (subsidiaryId.HasValue)
            query = query.Where(s => s.SubsidiaryId == subsidiaryId.Value);

        if (status.HasValue)
            query = query.Where(s => s.Status == status.Value);

        if (startDate.HasValue)
            query = query.Where(s => s.SaleDate >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(s => s.SaleDate <= endDate.Value);

        // Apply sorting
        query = sortBy.ToLower() switch
        {
            "saledate" => sortDirection.ToLower() == "asc" 
                ? query.OrderBy(s => s.SaleDate) 
                : query.OrderByDescending(s => s.SaleDate),
            "salenumber" => sortDirection.ToLower() == "asc" 
                ? query.OrderBy(s => s.SaleNumber) 
                : query.OrderByDescending(s => s.SaleNumber),
            "totalamount" => sortDirection.ToLower() == "asc" 
                ? query.OrderBy(s => s.TotalAmount) 
                : query.OrderByDescending(s => s.TotalAmount),
            "status" => sortDirection.ToLower() == "asc" 
                ? query.OrderBy(s => s.Status) 
                : query.OrderByDescending(s => s.Status),
            "createdat" => sortDirection.ToLower() == "asc" 
                ? query.OrderBy(s => s.CreatedAt) 
                : query.OrderByDescending(s => s.CreatedAt),
            _ => query.OrderByDescending(s => s.SaleDate)
        };

        // Get total count before pagination
        var totalCount = await query.CountAsync(cancellationToken);
        
        // Apply pagination
        var sales = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);

        return (sales, totalCount);
    }
}