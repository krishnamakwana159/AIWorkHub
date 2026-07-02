using System.Linq.Expressions;
using AIWorkHub.SharedKernel.Entities;

namespace AIWorkHub.Persistence.Repositories;

/// <summary>
/// Defines read and write operations for aggregate persistence.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
public interface IRepository<TEntity>
    where TEntity : BaseEntity
{
    /// <summary>
    /// Returns a queryable source for advanced composition.
    /// </summary>
    IQueryable<TEntity> Query();

    /// <summary>
    /// Gets an entity by identifier.
    /// </summary>
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists entities matching an optional predicate.
    /// </summary>
    Task<IReadOnlyList<TEntity>> ListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds an entity to the set.
    /// </summary>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an entity in the set.
    /// </summary>
    void Update(TEntity entity);

    /// <summary>
    /// Removes an entity from the set.
    /// </summary>
    void Remove(TEntity entity);
}
