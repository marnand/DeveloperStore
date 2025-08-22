using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ICustomerRepository using Entity Framework Core
/// </summary>
public class CustomerRepository : ICustomerRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of CustomerRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public CustomerRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new customer
    /// </summary>
    /// <param name="customer">The customer to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created customer</returns>
    public async Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync(cancellationToken);
        return customer;
    }

    /// <summary>
    /// Get a customer by unique identifier
    /// </summary>
    /// <param name="id">The unique identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Customer if found, null</returns>
    public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    /// <summary>
    /// Get a customer by email
    /// </summary>
    /// <param name="email">The email to search</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Customer if found, null</returns>
    public async Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
    }

    /// <summary>
    /// Get a customer by document
    /// </summary>
    /// <param name="document">The document to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Customer if found, null</returns>
    public async Task<Customer?> GetByDocumentAsync(string document, CancellationToken cancellationToken = default)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Document == document, cancellationToken);
    }

    /// <summary>
    /// Get all customers with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of customers</returns>
    public async Task<IEnumerable<Customer>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        return await _context.Customers
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Updates customer
    /// </summary>
    /// <param name="customer">Customer to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated customer</returns>
    public async Task<Customer> UpdateAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync(cancellationToken);
        return customer;
    }

    /// <summary>
    /// Deletes a customer
    /// </summary>
    /// <param name="id">Unique identifier of the customer</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the customer was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var customer = await GetByIdAsync(id, cancellationToken);
        if (customer == null)
            return false;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}