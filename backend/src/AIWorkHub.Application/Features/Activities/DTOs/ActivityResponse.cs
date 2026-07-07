namespace AIWorkHub.Application.Features.Activities.DTOs;

using AIWorkHub.Domain.Enums;

public sealed class ActivityResponse
{
    public Guid Id { get; set; }

    public ActivityEntityType EntityType { get; set; }

    public Guid EntityId { get; set; }

    public ActivityAction Action { get; set; }

    public string Description { get; set; } = string.Empty;

    public Guid? UserId { get; set; }

    public DateTimeOffset CreatedAtUtc { get; set; }
}
