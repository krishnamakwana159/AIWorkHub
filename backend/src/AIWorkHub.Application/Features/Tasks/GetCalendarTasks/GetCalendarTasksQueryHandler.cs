using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Features.Tasks.DTOs;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.GetCalendarTasks;

public sealed class GetCalendarTasksQueryHandler(
    IProjectRepository projectRepository,
    IWorkTaskRepository taskRepository,
    ICurrentUserService currentUser)
    : IRequestHandler<GetCalendarTasksQuery, Result<IReadOnlyList<CalendarTaskResponse>>>
{
    public async Task<Result<IReadOnlyList<CalendarTaskResponse>>> Handle(
        GetCalendarTasksQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUser.UserId, out var userId))
            return Result<IReadOnlyList<CalendarTaskResponse>>.Failure("Unauthorized.");

        var projects = await projectRepository.GetAllAsync(userId, cancellationToken);
        var projectIds = projects.Select(x => x.Id).ToList();

        var tasks = await taskRepository.GetByProjectIdsWithDueDateAsync(
            projectIds,
            cancellationToken);

        var response = tasks
            .Select(x => new CalendarTaskResponse
            {
                Id = x.Id,
                ProjectId = x.ProjectId,
                ProjectName = x.Project?.Name ?? string.Empty,
                Title = x.Title,
                Priority = x.Priority,
                Status = x.Status,
                DueDateUtc = x.DueDateUtc!.Value,
                AssigneeName = x.AssignedUser == null
                    ? null
                    : $"{x.AssignedUser.FirstName} {x.AssignedUser.LastName}".Trim()
            })
            .ToList();

        return Result<IReadOnlyList<CalendarTaskResponse>>.Success(response);
    }
}
