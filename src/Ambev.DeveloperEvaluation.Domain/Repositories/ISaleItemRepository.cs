using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface
/// </summary>
public interface ISaleItemRepository
{
    /// <summary>
    /// Creates a new sale item
    /// </summary>
    /// <param name="saleItem">Sale item</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created sale item</returns>
    Task<SaleItem> CreateAsync(SaleItem saleItem, CancellationToken cancellationToken = default);

    /// <summary>
    /// get a sale item by unique identifier
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Sale item if found, null</returns>
    Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// get all sale items for a specific sale
    /// </summary>
    /// <param name="saleId">SaleId to search</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of sale items for sale</returns>
    Task<IEnumerable<SaleItem>> GetBySaleIdAsync(Guid saleId, CancellationToken cancellationToken = default);

    /// <summary>
    /// get all sale items for a specific product
    /// </summary>
    /// <param name="productId">ProductId to search</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of sale items for the product</returns>
    Task<IEnumerable<SaleItem>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates
    /// </summary>
    /// <param name="saleItem">Sale item to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated sale item</returns>
    Task<SaleItem> UpdateAsync(SaleItem saleItem, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a sale
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the sale item was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}