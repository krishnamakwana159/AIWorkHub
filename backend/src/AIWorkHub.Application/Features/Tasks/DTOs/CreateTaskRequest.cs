using AIWorkHub.Domain.Enums;

namespace AIWorkHub.Application.Features.Tasks.DTOs;

public sealed class CreateTaskRequest
{
    public Guid ProjectId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public TaskPriority Priority { get; set; } = TaskPriority.Medium;

    public DateTime? StartDateUtc { get; set; }

    public DateTime? DueDateUtc { get; set; }

    public decimal EstimatedHours { get; set; }
}
