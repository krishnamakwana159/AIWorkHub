using AIWorkHub.Domain.Entities;

namespace AIWorkHub.Application.Interfaces.Repositories;

public interface INotificationRepository : IRepository<Notification>
{
    Task<List<Notification>> GetUserNotificationsAsync(
        Guid userId,
        CancellationToken cancellationToken);

    Task<int> GetUnreadCountAsync(
        Guid userId,
        CancellationToken cancellationToken);

    Task MarkAllReadAsync(
        Guid userId,
        CancellationToken cancellationToken);
}
