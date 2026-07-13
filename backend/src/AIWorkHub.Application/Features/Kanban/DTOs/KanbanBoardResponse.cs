namespace AIWorkHub.Application.Features.Kanban.DTOs;

public sealed class KanbanBoardResponse
{
    public Guid ProjectId { get; set; }

    public string ProjectName { get; set; } = string.Empty;

    public List<KanbanColumnDto> Columns { get; set; } = [];
}
