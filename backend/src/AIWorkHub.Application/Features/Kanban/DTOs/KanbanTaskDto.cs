using AIWorkHub.Domain.Enums;

namespace AIWorkHub.Application.Features.Kanban.DTOs;

public sealed class KanbanTaskDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public WorkTaskStatus Status { get; set; }

    public TaskPriority Priority { get; set; }

    public int Order { get; set; }

    public Guid? AssignedUserId { get; set; }

    public string? AssignedUserName { get; set; }

    public DateTime? DueDateUtc { get; set; }

    public decimal ActualHours { get; set; }

    public decimal EstimatedHours { get; set; }
}
