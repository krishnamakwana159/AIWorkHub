using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;

namespace AIWorkHub.Infrastructure.Services;

public sealed class NotificationService(
    INotificationRepository repository,
    IUnitOfWork unitOfWork,
    IRealtimeService realtimeService)
    : INotificationService
{
    public async Task NotifyAsync(
        Guid userId,
        string title,
        string message,
        NotificationType type,
        string? navigationUrl,
        CancellationToken cancellationToken)
    {
        var notification = new Notification
        {
            UserId = userId,
            Title = title,
            Message = message,
            Type = type,
            NavigationUrl = navigationUrl,
            IsRead = false
        };

        await repository.AddAsync(
            notification,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await realtimeService.SendToUserAsync(
            userId,
            "NotificationCreated",
            new
            {
                notification.Id,
                notification.Title,
                notification.Message,
                notification.Type,
                notification.NavigationUrl,
                notification.CreatedAtUtc
            },
            cancellationToken);
    }
}
