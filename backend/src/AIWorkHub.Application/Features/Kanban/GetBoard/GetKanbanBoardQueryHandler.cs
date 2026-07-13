using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Features.Kanban.DTOs;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Results;
using AutoMapper;
using MediatR;

namespace AIWorkHub.Application.Features.Kanban.GetBoard;

public sealed class GetKanbanBoardQueryHandler(
    IProjectRepository projectRepository,
    IWorkTaskRepository taskRepository,
    IMapper mapper)
    : IRequestHandler<GetKanbanBoardQuery, Result<KanbanBoardResponse>>
{
    public async Task<Result<KanbanBoardResponse>> Handle(
        GetKanbanBoardQuery request,
        CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(
            request.ProjectId,
            cancellationToken);

        if (project is null)
            return Result<KanbanBoardResponse>.Failure("Project not found.");

        var tasks = await taskRepository.GetKanbanTasksAsync(
            request.ProjectId,
            cancellationToken);

        var board = new KanbanBoardResponse
        {
            ProjectId = project.Id,
            ProjectName = project.Name
        };

        foreach (var status in Enum.GetValues<WorkTaskStatus>())
        {
            board.Columns.Add(new KanbanColumnDto
            {
                Status = status,
                Title = status.ToString(),
                Count = tasks.Count(t => t.Status == status),
                Tasks = mapper.Map<List<KanbanTaskDto>>(
                    tasks.Where(t => t.Status == status).ToList())
            });
        }

        return Result<KanbanBoardResponse>.Success(board);
    }
}
