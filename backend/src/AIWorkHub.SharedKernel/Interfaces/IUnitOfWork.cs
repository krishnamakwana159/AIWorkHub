namespace AIWorkHub.SharedKernel.Interfaces;

/// <summary>
/// Coordinates persistence changes.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Persists pending changes.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
