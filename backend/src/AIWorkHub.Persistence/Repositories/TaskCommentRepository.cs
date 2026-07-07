using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIWorkHub.Persistence.Repositories;

public sealed class TaskCommentRepository(AppDbContext dbContext)
    : RepositoryBase<TaskComment>(dbContext), ITaskCommentRepository
{
    public async Task<IReadOnlyList<TaskComment>> GetByTaskIdAsync(
        Guid taskId,
        CancellationToken cancellationToken)
    {
        return await DbSet
            .Include(x => x.User)
            .Where(x => x.TaskId == taskId)
            .OrderBy(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> CommentExistsAsync(
        Guid commentId,
        CancellationToken cancellationToken)
    {
        return await DbSet.AnyAsync(
            x => x.Id == commentId,
            cancellationToken);
    }
}
