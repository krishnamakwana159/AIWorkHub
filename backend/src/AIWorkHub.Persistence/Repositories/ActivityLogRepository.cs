using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIWorkHub.Persistence.Repositories;

public sealed class ActivityLogRepository(AppDbContext dbContext)
    : RepositoryBase<ActivityLog>(dbContext), IActivityLogRepository
{
    public async Task<IReadOnlyList<ActivityLog>> GetEntityActivitiesAsync(
        Guid entityId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(x => x.User)
            .Where(x => x.EntityId == entityId)
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }
}
