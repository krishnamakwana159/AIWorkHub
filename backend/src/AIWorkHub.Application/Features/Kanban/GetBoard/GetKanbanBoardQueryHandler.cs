using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Features.Kanban.DTOs;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using AutoMapper;
using MediatR;

namespace AIWorkHub.Application.Features.Kanban.GetBoard;

public sealed class GetKanbanBoardQueryHandler(
    IProjectRepository projectRepository,
    IProjectMemberRepository projectMemberRepository,
    IWorkTaskRepository taskRepository,
    ICurrentUserService currentUser,
    IMapper mapper)
    : IRequestHandler<GetKanbanBoardQuery, Result<KanbanBoardResponse>>
{
    public async Task<Result<KanbanBoardResponse>> Handle(
        GetKanbanBoardQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUser.UserId, out var userId))
            return Result<KanbanBoardResponse>.Failure("Unauthorized.");

        var project = await projectRepository.GetByIdAsync(
            request.ProjectId,
            cancellationToken);

        if (project is null)
            return Result<KanbanBoardResponse>.Failure("Project not found.");

        if (!currentUser.IsAdministrator)
        {
            var isOwner = project.OwnerId == userId;

            var isMember = !isOwner && await projectMemberRepository.ExistsAsync(
                request.ProjectId,
                userId,
                cancellationToken);

            if (!isOwner && !isMember)
                return Result<KanbanBoardResponse>.Failure(
                    "You do not have access to this project.");
        }

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
