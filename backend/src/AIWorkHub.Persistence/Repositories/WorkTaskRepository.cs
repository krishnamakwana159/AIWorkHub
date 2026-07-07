using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIWorkHub.Persistence.Repositories;

public sealed class WorkTaskRepository(AppDbContext dbContext)
    : RepositoryBase<WorkTask>(dbContext), IWorkTaskRepository
{
    public async Task<bool> WorkTaskExistsAsync(
        Guid projectId,
        string title,
        CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(
            x => x.ProjectId == projectId &&
                 x.Title == title &&
                 !x.IsDeleted,
            cancellationToken);
    }

    public async Task<IReadOnlyList<WorkTask>> GetByProjectAsync(
        Guid projectId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(x => x.ProjectId == projectId)
            .OrderBy(x => x.Order)
            .ToListAsync(cancellationToken);
    }
}
