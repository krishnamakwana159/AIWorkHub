using AIWorkHub.Application.Features.Tasks.DTOs;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using AutoMapper;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.UpdateTask;

public sealed class UpdateTaskCommandHandler(
    IWorkTaskRepository repository,
    IActivityService activityService,
    ICurrentUserService currentUserService,
    INotificationService notificationService,
    IUnitOfWork unitOfWork,
    IRealtimeService realtimeService,
    IMapper mapper)
    : IRequestHandler<UpdateTaskCommand, Result<TaskResponse>>
{
    public async Task<Result<TaskResponse>> Handle(
        UpdateTaskCommand request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUserService.UserId, out var currentUserId))
        {
            return Result<TaskResponse>.Failure("User not found.");
        }

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

        if (task.Status == WorkTaskStatus.Completed)
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

        if (task.CreatedBy != currentUserService.UserId)
        {
            await notificationService.NotifyAsync(
                currentUserId,
                "Task Status Updated",
                $"Task '{task.Title}' is now {task.Status}.",
                NotificationType.TaskUpdated,
                $"/tasks/{task.Id}",
                cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await realtimeService.SendToProjectAsync(
            task.ProjectId,
            "TaskUpdated",
            new
            {
                task.Id,
                task.Title,
                task.Status
            },
            cancellationToken);

        return Result<TaskResponse>.Success(
            mapper.Map<TaskResponse>(task));

    }
}
