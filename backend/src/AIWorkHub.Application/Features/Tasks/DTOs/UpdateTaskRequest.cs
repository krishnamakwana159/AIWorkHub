using AIWorkHub.Domain.Enums;
using TaskStatus = AIWorkHub.Domain.Enums.TaskStatus;

namespace AIWorkHub.Application.Features.Tasks.DTOs;

public sealed class UpdateTaskRequest
{
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public TaskPriority Priority { get; set; }

    public TaskStatus Status { get; set; }

    public DateTime? StartDateUtc { get; set; }

    public DateTime? DueDateUtc { get; set; }

    public decimal EstimatedHours { get; set; }

    public decimal ActualHours { get; set; }
}
