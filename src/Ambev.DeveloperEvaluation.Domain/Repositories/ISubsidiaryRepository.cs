using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface Subsidiary
/// </summary>
public interface ISubsidiaryRepository
{
    /// <summary>
    /// Creates a new Subsidiary
    /// </summary>
    /// <param name="Subsidiary">subsidiary to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created Subsidiary</returns>
    Task<Subsidiary> CreateAsync(Subsidiary Subsidiary, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a Subsidiary by unique identifier
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Subsidiary if found, null</returns>
    Task<Subsidiary?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// get all Subsidiaryes with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of Subsidiaryes</returns>
    Task<IEnumerable<Subsidiary>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates
    /// </summary>
    /// <param name="Subsidiary">Subsidiary to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated Subsidiary</returns>
    Task<Subsidiary> UpdateAsync(Subsidiary Subsidiary, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a Subsidiary
    /// </summary>
    /// <param name="id">unique identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Subsidiary was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}