using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.Assign;

public sealed class AssignTaskCommandHandler(
    IWorkTaskRepository taskRepository,
    IUserRepository userRepository,
    IActivityService activityService,
    INotificationService notificationService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AssignTaskCommand, Result>
{
    public async Task<Result> Handle(
        AssignTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(
            request.TaskId,
            cancellationToken);

        if (task is null)
            return Result.Failure("Task not found.");

        var user = await userRepository.GetByIdAsync(
            request.UserId,
            cancellationToken);

        if (user is null)
            return Result.Failure("User not found.");

        task.AssignedUserId = request.UserId;

        taskRepository.Update(task);

        await notificationService.NotifyAsync(
            user.Id,
            "Task Assigned",
            $"You have been assigned '{task.Title}'.",
            NotificationType.TaskAssigned,
            $"/tasks/{task.Id}",
            cancellationToken);

        await activityService.LogAsync(
            ActivityEntityType.Task,
            task.Id,
            ActivityAction.Assigned,
            $"Task assigned to user '{user.Email}'.",
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
