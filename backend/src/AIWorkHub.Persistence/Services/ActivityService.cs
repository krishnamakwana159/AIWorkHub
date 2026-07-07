using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;

namespace AIWorkHub.Persistence.Services;

public sealed class ActivityService(
    IActivityLogRepository repository,
    ICurrentUserService currentUserService)
    : IActivityService
{
    public async Task LogAsync(
        ActivityEntityType entityType,
        Guid entityId,
        ActivityAction action,
        string description,
        CancellationToken cancellationToken = default)
    {
        Guid? userId = null;

        if (Guid.TryParse(currentUserService.UserId, out var id))
            userId = id;

        var activity = new ActivityLog
        {
            EntityType = entityType,
            EntityId = entityId,
            Action = action,
            Description = description,
            UserId = userId
        };

        await repository.AddAsync(activity, cancellationToken);
    }
}
