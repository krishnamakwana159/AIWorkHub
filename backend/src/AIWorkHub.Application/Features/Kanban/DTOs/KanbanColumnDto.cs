using AIWorkHub.Domain.Enums;

namespace AIWorkHub.Application.Features.Kanban.DTOs;

public sealed class KanbanColumnDto
{
    public WorkTaskStatus Status { get; set; }

    public string Title { get; set; } = string.Empty;

    public int Count { get; set; }

    public List<KanbanTaskDto> Tasks { get; set; } = [];
}
