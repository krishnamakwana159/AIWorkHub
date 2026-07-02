using AIWorkHub.SharedKernel.Entities;

namespace AIWorkHub.Persistence.Repositories;

/// <summary>
/// Generic repository for aggregate persistence.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
public sealed class GenericRepository<TEntity>(AppDbContext dbContext) : RepositoryBase<TEntity>(dbContext)
    where TEntity : BaseEntity;
