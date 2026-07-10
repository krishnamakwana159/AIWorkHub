using AIWorkHub.Application.Features.Tasks.Comments.DTOs;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.UpdateStatus;

public sealed class UpdateTaskStatusCommandHandler(
    IWorkTaskRepository taskRepository,
    INotificationService notificationService,
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateTaskStatusCommand, Result>
{
    public async Task<Result> Handle(
        UpdateTaskStatusCommand request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUserService.UserId, out var currentUserId))
        {
            return Result<CommentResponse>.Failure("User not found.");
        }

        var task = await taskRepository.GetByIdAsync(
            request.TaskId,
            cancellationToken);

        if (task is null)
            return Result.Failure("Task not found.");

        if (task.Status == request.Status)
            return Result.Success();

        task.Status = request.Status;

        if (request.Status == WorkTaskStatus.Completed)
        {
            task.CompletedAtUtc = DateTime.UtcNow;
        }
        else
        {
            task.CompletedAtUtc = null;
        }

        taskRepository.Update(task);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await notificationService.NotifyAsync(
            currentUserId,
            "Task Status Updated",
            $"Task '{task.Title}' is now {task.Status}.",
            NotificationType.TaskUpdated,
            $"/tasks/{task.Id}",
            cancellationToken);

        return Result.Success();
    }
}
