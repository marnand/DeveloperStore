using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ISubsidiaryRepository using Entity Framework Core
/// </summary>
public class SubsidiaryRepository : ISubsidiaryRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of SubsidiaryRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SubsidiaryRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new Subsidiary
    /// </summary>
    /// <param name="subsidiary">subsidiary to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created Subsidiary</returns>
    public async Task<Subsidiary> CreateAsync(Subsidiary subsidiary, CancellationToken cancellationToken = default)
    {
        _context.Subsidiaries.Add(subsidiary);
        await _context.SaveChangesAsync(cancellationToken);
        return subsidiary;
    }

    /// <summary>
    /// Get a Subsidiary by unique identifier
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Subsidiary if found, null</returns>
    public async Task<Subsidiary?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Subsidiaries.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    /// <summary>
    /// get all Subsidiaries with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of Subsidiaries</returns>
    public async Task<IEnumerable<Subsidiary>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        return await _context.Subsidiaries
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Updates
    /// </summary>
    /// <param name="subsidiary">Subsidiary to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated Subsidiary</returns>
    public async Task<Subsidiary> UpdateAsync(Subsidiary subsidiary, CancellationToken cancellationToken = default)
    {
        _context.Subsidiaries.Update(subsidiary);
        await _context.SaveChangesAsync(cancellationToken);
        return subsidiary;
    }

    /// <summary>
    /// Deletes a Subsidiary
    /// </summary>
    /// <param name="id">unique identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Subsidiary was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var subsidiary = await GetByIdAsync(id, cancellationToken);
        if (subsidiary == null)
            return false;

        _context.Subsidiaries.Remove(subsidiary);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}