using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIWorkHub.Persistence.Repositories;

public sealed class NotificationRepository(AppDbContext context)
    : RepositoryBase<Notification>(context), INotificationRepository
{
    public async Task<List<Notification>> GetUserNotificationsAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await context.Notifications
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public Task<int> GetUnreadCountAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return context.Notifications
            .CountAsync(
                x => x.UserId == userId &&
                     !x.IsRead,
                cancellationToken);
    }

    public async Task MarkAllReadAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var notifications = await context.Notifications
            .Where(x => x.UserId == userId && !x.IsRead)
            .ToListAsync(cancellationToken);

        foreach (var notification in notifications)
            notification.IsRead = true;
    }
}
