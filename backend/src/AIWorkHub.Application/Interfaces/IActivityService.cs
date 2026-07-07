using AIWorkHub.Domain.Enums;

namespace AIWorkHub.Application.Interfaces;

public interface IActivityService
{
    Task LogAsync(
        ActivityEntityType entityType,
        Guid entityId,
        ActivityAction action,
        string description,
        CancellationToken cancellationToken = default);
}
