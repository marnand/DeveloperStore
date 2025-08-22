using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Product entity operations
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="product">The product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Product</returns>
    Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a product by unique identifier
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>product if found, null</returns>
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a product by their SKU
    /// </summary>
    /// <param name="sku">The SKU identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null</returns>
    Task<Product?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default);

    /// <summary>
    /// get all products with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of products</returns>
    Task<IEnumerable<Product>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates product
    /// </summary>
    /// <param name="product">product to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated product</returns>
    Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a product
    /// </summary>
    /// <param name="id">Unique identifier of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the product was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}