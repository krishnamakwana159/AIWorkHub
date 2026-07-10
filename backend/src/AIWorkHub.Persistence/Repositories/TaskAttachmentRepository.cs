using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIWorkHub.Persistence.Repositories;

public sealed class TaskAttachmentRepository(AppDbContext dbContext)
    : RepositoryBase<TaskAttachment>(dbContext), ITaskAttachmentRepository
{
    public async Task<IReadOnlyList<TaskAttachment>> GetByTaskIdAsync(
        Guid taskId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Include(x => x.UploadedByUser)
            .Where(x => x.TaskId == taskId)
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<TaskAttachment?> GetByIdWithUserAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(x => x.UploadedByUser)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }
}
