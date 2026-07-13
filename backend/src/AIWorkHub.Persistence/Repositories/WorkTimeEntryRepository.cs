namespace AIWorkHub.Persistence.Repositories;

using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public sealed class WorkTimeEntryRepository(AppDbContext dbContext)
    : RepositoryBase<WorkTimeEntry>(dbContext), IWorkTimeEntryRepository
{

    public async Task<WorkTimeEntry?> GetRunningTimerAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await DbSet
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                x.IsRunning,
                cancellationToken);
    }

    public async Task<List<WorkTimeEntry>> GetTaskEntriesAsync(
        Guid workTaskId,
        CancellationToken cancellationToken)
    {
        return await dbContext.WorkTimeEntries
            .Include(x => x.User)
            .Where(x => x.WorkTaskId == workTaskId)
            .OrderByDescending(x => x.StartTimeUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<WorkTimeEntry>> GetUserEntriesAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await dbContext.WorkTimeEntries
            .Include(x => x.WorkTask)
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.StartTimeUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<WorkTimeEntry>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await DbSet
            .ToListAsync(cancellationToken);
    }
}
