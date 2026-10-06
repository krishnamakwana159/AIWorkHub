using AIWorkHub.Domain.Enums;

namespace AIWorkHub.Application.Features.Tasks.DTOs;

public sealed class MyTaskResponse
{
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }

    public string ProjectName { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public TaskPriority Priority { get; set; }

    public WorkTaskStatus Status { get; set; }

    public DateTime? DueDateUtc { get; set; }

    public string? AssigneeName { get; set; }
}
