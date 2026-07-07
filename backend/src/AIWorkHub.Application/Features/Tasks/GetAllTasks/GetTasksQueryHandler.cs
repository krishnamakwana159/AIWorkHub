using AIWorkHub.Application.Features.Tasks.DTOs;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.GetAllTasks;

public sealed class GetTasksQueryHandler(
    IWorkTaskRepository repository)
    : IRequestHandler<GetTasksQuery, Result<IReadOnlyList<TaskResponse>>>
{
    public async Task<Result<IReadOnlyList<TaskResponse>>> Handle(
        GetTasksQuery request,
        CancellationToken cancellationToken)
    {
        var tasks = await repository.GetByProjectAsync(
            request.ProjectId,
            cancellationToken);

        var response = tasks
            .Select(x => new TaskResponse
            {
                Id = x.Id,
                ProjectId = x.ProjectId,
                Title = x.Title,
                Description = x.Description,
                Priority = x.Priority,
                Status = x.Status,
                EstimatedHours = x.EstimatedHours,
                ActualHours = x.ActualHours,
                StartDateUtc = x.StartDateUtc,
                DueDateUtc = x.DueDateUtc,
                CompletedAtUtc = x.CompletedAtUtc,
                IsPinned = x.IsPinned,
                IsFavorite = x.IsFavorite,
                Order = x.Order
            })
            .ToList();

        return Result<IReadOnlyList<TaskResponse>>.Success(response);
    }
}
