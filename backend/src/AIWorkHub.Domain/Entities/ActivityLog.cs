using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Entities;

namespace AIWorkHub.Domain.Entities;

public sealed class ActivityLog : AuditableEntity
{
    public ActivityEntityType EntityType { get; set; }

    public Guid EntityId { get; set; }

    public ActivityAction Action { get; set; }

    public string Description { get; set; } = string.Empty;

    public Guid? UserId { get; set; }

    public User? User { get; set; }
}
