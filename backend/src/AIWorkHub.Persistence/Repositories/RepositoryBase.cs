using System.Linq.Expressions;
using AIWorkHub.Application.Common.Specifications;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Persistence.Specifications.Base;
using AIWorkHub.SharedKernel.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIWorkHub.Persistence.Repositories;

/// <summary>
/// Base EF Core repository implementation.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
public abstract class RepositoryBase<TEntity>(AppDbContext dbContext) : IRepository<TEntity>
    where TEntity : BaseEntity
{
    protected DbSet<TEntity> DbSet => dbContext.Set<TEntity>();

    /// <inheritdoc />
    public IQueryable<TEntity> Query()
    {
        return DbSet.AsNoTracking();
    }

    /// <inheritdoc />
    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FindAsync([id], cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<TEntity>> ListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        var query = DbSet.AsNoTracking();

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    /// <inheritdoc />
    public void Remove(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public async Task<IReadOnlyList<TEntity>> ListAsync(
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken)
    {
        var query = SpecificationEvaluator.GetQuery(
            DbSet.AsQueryable(),
            specification);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> FirstOrDefaultAsync(
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken)
    {
        var query = SpecificationEvaluator.GetQuery(
            DbSet.AsQueryable(),
            specification);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }
    public async Task<int> CountAsync(
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken)
    {
        var query = SpecificationEvaluator.GetQuery(
            DbSet.AsQueryable(),
            specification);

        return await query.CountAsync(cancellationToken);
    }
}
