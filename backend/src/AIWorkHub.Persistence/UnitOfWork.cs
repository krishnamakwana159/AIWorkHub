using AIWorkHub.SharedKernel.Interfaces;

namespace AIWorkHub.Persistence;

/// <summary>
/// EF Core unit of work implementation.
/// </summary>
public sealed class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    /// <inheritdoc />
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
