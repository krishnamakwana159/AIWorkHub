using AIWorkHub.Application.Features.Tasks.DTOs;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.GetTaskById;

public sealed class GetTaskByIdQueryHandler(
    IWorkTaskRepository repository)
    : IRequestHandler<GetTaskByIdQuery, Result<TaskResponse>>
{
    public async Task<Result<TaskResponse>> Handle(
        GetTaskByIdQuery request,
        CancellationToken cancellationToken)
    {
        var task = await repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (task is null)
            return Result<TaskResponse>.Failure("Task not found.");

        return Result<TaskResponse>.Success(new TaskResponse
        {
            Id = task.Id,
            ProjectId = task.ProjectId,
            Title = task.Title,
            Description = task.Description,
            Priority = task.Priority,
            Status = task.Status,
            EstimatedHours = task.EstimatedHours,
            ActualHours = task.ActualHours,
            StartDateUtc = task.StartDateUtc,
            DueDateUtc = task.DueDateUtc,
            CompletedAtUtc = task.CompletedAtUtc,
            IsPinned = task.IsPinned,
            IsFavorite = task.IsFavorite,
            Order = task.Order
        });
    }
}
