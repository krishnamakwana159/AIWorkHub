using AIWorkHub.Domain.Enums;

namespace AIWorkHub.Application.Features.Projects.DTOs;

public sealed class UpdateProjectRequest
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string Color { get; set; } = "#2563EB";

    public ProjectPriority Priority { get; set; }

    public ProjectStatus Status { get; set; }

    public DateTime? StartDateUtc { get; set; }

    public DateTime? TargetCompletionDateUtc { get; set; }

    public bool IsFavorite { get; set; }
}
