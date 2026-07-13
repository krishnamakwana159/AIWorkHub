using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Kanban.MoveTask;

public sealed class MoveTaskCommandHandler(
    IWorkTaskRepository taskRepository,
    IActivityService activityService,
    INotificationService notificationService,
    IRealtimeService realtimeService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<MoveTaskCommand, Result>
{
    public async Task<Result> Handle(
        MoveTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(
            request.TaskId,
            cancellationToken);

        if (task is null)
            return Result.Failure("Task not found.");

        var oldStatus = task.Status;

        if (oldStatus == request.Request.Status)
        {
            var tasks = await taskRepository.GetOrderedTasksAsync(
                task.ProjectId,
                task.Status,
                cancellationToken);

            tasks.RemoveAll(x => x.Id == task.Id);

            var newOrder = Math.Clamp(
                request.Request.Order,
                0,
                tasks.Count);

            tasks.Insert(newOrder, task);

            NormalizeOrder(tasks);

            await taskRepository.UpdateRangeAsync(
                tasks,
                cancellationToken);
        }
        else
        {
            var sourceTasks =
                await taskRepository.GetOrderedTasksAsync(
                    task.ProjectId,
                    oldStatus,
                    cancellationToken);

            var destinationTasks =
                await taskRepository.GetOrderedTasksAsync(
                    task.ProjectId,
                    request.Request.Status,
                    cancellationToken);

            sourceTasks.RemoveAll(x => x.Id == task.Id);

            NormalizeOrder(sourceTasks);

            task.Status = request.Request.Status;

            var destinationOrder = Math.Clamp(
                request.Request.Order,
                0,
                destinationTasks.Count);

            destinationTasks.Insert(destinationOrder, task);

            NormalizeOrder(destinationTasks);

            await taskRepository.UpdateRangeAsync(
                sourceTasks,
                cancellationToken);

            await taskRepository.UpdateRangeAsync(
                destinationTasks,
                cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await activityService.LogAsync(
            ActivityEntityType.Task,
            task.Id,
            ActivityAction.Updated,
            $"Moved task to {task.Status}.",
            cancellationToken);

        if (task.AssignedUserId.HasValue)
        {
            await notificationService.NotifyAsync(
                task.AssignedUserId.Value,
                "Task Updated",
                $"'{task.Title}' moved to {task.Status}.",
                NotificationType.TaskUpdated,
                $"/tasks/{task.Id}",
                cancellationToken);
        }

        await realtimeService.SendToProjectAsync(
            task.ProjectId,
            "TaskMoved",
            new
            {
                task.Id,
                task.Status,
                task.Order
            },
            cancellationToken);

        return Result.Success();
    }

    private static void NormalizeOrder(List<WorkTask> tasks)
    {
        for (var i = 0; i < tasks.Count; i++)
        {
            tasks[i].Order = i;
        }
    }

}
