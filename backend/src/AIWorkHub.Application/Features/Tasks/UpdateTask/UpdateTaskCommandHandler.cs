using AIWorkHub.Application.Features.Tasks.DTOs;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.UpdateTask;

public sealed class UpdateTaskCommandHandler(
    IWorkTaskRepository repository,
    IActivityService activityService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateTaskCommand, Result<TaskResponse>>
{
    public async Task<Result<TaskResponse>> Handle(
        UpdateTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (task is null)
            return Result<TaskResponse>.Failure("Task not found.");

        task.Title = request.Request.Title;
        task.Description = request.Request.Description;
        task.Priority = request.Request.Priority;
        task.Status = request.Request.Status;
        task.StartDateUtc = request.Request.StartDateUtc;
        task.DueDateUtc = request.Request.DueDateUtc;
        task.EstimatedHours = request.Request.EstimatedHours;
        task.ActualHours = request.Request.ActualHours;

        if (task.Status == Domain.Enums.TaskStatus.Completed)
            task.CompletedAtUtc = DateTime.UtcNow;
        else
            task.CompletedAtUtc = null;

        repository.Update(task);

        await activityService.LogAsync(
            ActivityEntityType.Task,
            task.Id,
            ActivityAction.StatusChanged,
            $"Status changed to {task.Status}.",
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

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
            IsFavorite = task.IsFavorite,
            IsPinned = task.IsPinned,
            Order = task.Order
        });
    }
}
