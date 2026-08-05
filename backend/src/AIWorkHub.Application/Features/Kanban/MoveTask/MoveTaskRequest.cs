using AIWorkHub.Domain.Enums;

namespace AIWorkHub.Application.Features.Kanban.MoveTask;

public sealed class MoveTaskRequest
{
    public Guid ProjectId { get; set; }

    public WorkTaskStatus Status { get; set; }

    public int Order { get; set; }
}
