using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Entities;

namespace AIWorkHub.Domain.Entities;
public class Notification : AuditableEntity
{
    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public NotificationType Type { get; set; }

    public bool IsRead { get; set; }

    public string? NavigationUrl { get; set; }
}
