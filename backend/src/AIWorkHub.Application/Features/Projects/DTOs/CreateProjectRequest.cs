using AIWorkHub.Domain.Enums;

namespace AIWorkHub.Application.Features.Projects.DTOs;
public sealed class CreateProjectRequest
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string Color { get; set; } = "#2563EB";

    public ProjectPriority Priority { get; set; } = ProjectPriority.Medium;

    public DateTime? StartDateUtc { get; set; }

    public DateTime? TargetCompletionDateUtc { get; set; }
}
