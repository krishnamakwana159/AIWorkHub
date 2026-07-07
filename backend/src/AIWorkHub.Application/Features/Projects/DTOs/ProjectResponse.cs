using AIWorkHub.Domain.Enums;

namespace AIWorkHub.Application.Features.Projects.DTOs;

public sealed class ProjectResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string Color { get; set; } = string.Empty;

    public ProjectStatus Status { get; set; }

    public ProjectPriority Priority { get; set; }

    public bool IsArchived { get; set; }

    public bool IsFavorite { get; set; }

    public decimal Progress { get; set; }

    public DateTime? StartDateUtc { get; set; }

    public DateTime? TargetCompletionDateUtc { get; set; }

    public DateTime? CompletedAtUtc { get; set; }

    public DateTimeOffset CreatedAtUtc { get; set; }
}
