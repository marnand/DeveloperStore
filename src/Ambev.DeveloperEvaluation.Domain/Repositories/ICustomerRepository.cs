using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Customer
/// </summary>
public interface ICustomerRepository
{
    /// <summary>
    /// Creates a new customer
    /// </summary>
    /// <param name="customer">The customer to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created customer</returns>
    Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a customer by unique identifier
    /// </summary>
    /// <param name="id">The unique identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Customer if found, null</returns>
    Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a customer by email
    /// </summary>
    /// <param name="email">The email to search</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Customer if found, null</returns>
    Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a customer by document
    /// </summary>
    /// <param name="document">The document to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Customer if found, null</returns>
    Task<Customer?> GetByDocumentAsync(string document, CancellationToken cancellationToken = default);

    /// <summary>
    /// get all customers with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of customers</returns>
    Task<IEnumerable<Customer>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    /// <summary>
    /// update customer
    /// </summary>
    /// <param name="customer">Customer to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated customer</returns>
    Task<Customer> UpdateAsync(Customer customer, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a customer
    /// </summary>
    /// <param name="id">The unique identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if customer was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}