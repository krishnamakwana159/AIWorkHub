using AIWorkHub.Domain.Enums;

namespace AIWorkHub.Application.Features.Tasks.DTOs;

public sealed class AdminTaskResponse
{
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }

    public string ProjectName { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public TaskPriority Priority { get; set; }

    public WorkTaskStatus Status { get; set; }

    public bool IsPinned { get; set; }

    public bool IsFavorite { get; set; }

    public DateTime? StartDateUtc { get; set; }

    public DateTime? DueDateUtc { get; set; }

    public DateTime? CompletedAtUtc { get; set; }

    public Guid? AssigneeId { get; set; }

    public string? AssigneeName { get; set; }
}
