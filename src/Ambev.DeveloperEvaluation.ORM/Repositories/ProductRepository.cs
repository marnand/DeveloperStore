using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IProductRepository using Entity Framework Core
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of ProductRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="product">The product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Product</returns>
    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    /// <summary>
    /// Get a product by unique identifier
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>product if found, null</returns>
    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    /// <summary>
    /// Get a product by their SKU
    /// </summary>
    /// <param name="sku">The SKU identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null</returns>
    public async Task<Product?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Sku == sku, cancellationToken);
    }

    /// <summary>
    /// get all products with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of products</returns>
    public async Task<IEnumerable<Product>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Updates product
    /// </summary>
    /// <param name="product">product to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated product</returns>
    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    /// <summary>
    /// Deletes a product
    /// </summary>
    /// <param name="id">Unique identifier of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the product was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await GetByIdAsync(id, cancellationToken);
        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}