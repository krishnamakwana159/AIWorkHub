using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using AIWorkHub.Domain.Enums;
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

    public async Task<List<WorkTask>> GetKanbanTasksAsync(
        Guid projectId,
        CancellationToken cancellationToken)
    {
        return await DbSet
            .Include(x => x.AssignedUser)
            .Where(x => x.ProjectId == projectId)
            .OrderBy(x => x.Status)
            .ThenBy(x => x.Order)
            .ToListAsync(cancellationToken);
    }


    public async Task<List<WorkTask>> GetTasksByStatusAsync(
        Guid projectId,
        WorkTaskStatus status,
        CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(x =>
                x.ProjectId == projectId &&
                x.Status == status)
            .OrderBy(x => x.Order)
            .ToListAsync(cancellationToken);
    }

    public Task UpdateRangeAsync(
        IEnumerable<WorkTask> tasks,
        CancellationToken cancellationToken)
    {
        DbSet.UpdateRange(tasks);

        return Task.CompletedTask;
    }

    public async Task<List<WorkTask>> GetOrderedTasksAsync(
        Guid projectId,
        WorkTaskStatus status,
        CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(x =>
                x.ProjectId == projectId &&
                x.Status == status)
            .OrderBy(x => x.Order)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<WorkTask>> GetAllTasksAsync(
        CancellationToken cancellationToken)
    {
        return await DbSet
            .Include(x => x.AssignedUser)
            .ToListAsync(cancellationToken);
    }
}
