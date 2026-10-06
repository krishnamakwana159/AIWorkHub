using AIWorkHub.Application.Features.Tasks.DTOs;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.GetAllTasksAcrossProjects;

public sealed class GetAllTasksAcrossProjectsQueryHandler(
    IWorkTaskRepository repository)
    : IRequestHandler<GetAllTasksAcrossProjectsQuery, Result<IReadOnlyList<AdminTaskResponse>>>
{
    public async Task<Result<IReadOnlyList<AdminTaskResponse>>> Handle(
        GetAllTasksAcrossProjectsQuery request,
        CancellationToken cancellationToken)
    {
        var tasks = await repository.GetAllTasksWithProjectAsync(cancellationToken);

        var response = tasks
            .Select(x => new AdminTaskResponse
            {
                Id = x.Id,
                ProjectId = x.ProjectId,
                ProjectName = x.Project?.Name ?? string.Empty,
                Title = x.Title,
                Description = x.Description,
                Priority = x.Priority,
                Status = x.Status,
                StartDateUtc = x.StartDateUtc,
                DueDateUtc = x.DueDateUtc,
                CompletedAtUtc = x.CompletedAtUtc,
                IsPinned = x.IsPinned,
                IsFavorite = x.IsFavorite,
                AssigneeId = x.AssignedUserId,
                AssigneeName = x.AssignedUser == null
                    ? null
                    : $"{x.AssignedUser.FirstName} {x.AssignedUser.LastName}".Trim()
            })
            .ToList();

        return Result<IReadOnlyList<AdminTaskResponse>>.Success(response);
    }
}
