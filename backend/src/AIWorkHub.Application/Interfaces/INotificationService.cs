using AIWorkHub.Domain.Enums;

namespace AIWorkHub.Application.Interfaces;

public interface INotificationService
{
    Task NotifyAsync(
        Guid userId,
        string title,
        string message,
        NotificationType type,
        string? navigationUrl,
        CancellationToken cancellationToken);
}
