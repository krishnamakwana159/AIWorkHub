using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIWorkHub.Persistence.Repositories;

public sealed class TaskTagRepository(AppDbContext context)
    : RepositoryBase<TaskTag>(context), ITaskTagRepository
{
    public async Task<TaskTag?> GetAsync(
        Guid workTaskId,
        Guid tagId,
        CancellationToken cancellationToken)
    {
        return await context.TaskTags.FirstOrDefaultAsync(
            x => x.WorkTaskId == workTaskId && x.TagId == tagId,
            cancellationToken);
    }

    public async Task<List<TaskTag>> GetByTaskIdAsync(
        Guid workTaskId,
        CancellationToken cancellationToken)
    {
        return await context.TaskTags
            .Include(x => x.Tag)
            .Where(x => x.WorkTaskId == workTaskId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TaskTag>> GetByTaskIdsAsync(
        List<Guid> workTaskIds,
        CancellationToken cancellationToken)
    {
        if (workTaskIds.Count == 0)
            return new List<TaskTag>();

        return await context.TaskTags
            .Include(x => x.Tag)
            .Where(x => workTaskIds.Contains(x.WorkTaskId))
            .ToListAsync(cancellationToken);
    }
}
