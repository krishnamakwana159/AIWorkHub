using AIWorkHub.Domain.Enums;

namespace AIWorkHub.Application.Features.Notifications.DTOs;

public sealed class NotificationResponse
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; }

    public string? NavigationUrl { get; set; }

    public NotificationType Type { get; set; }

    public DateTimeOffset CreatedAtUtc { get; set; }
}
