using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Kanban.MoveTask;

public sealed class MoveTaskCommandHandler(
    IWorkTaskRepository repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<MoveTaskCommand, Result>
{
    public async Task<Result> Handle(
        MoveTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await repository.GetByIdAsync(
            request.TaskId,
            cancellationToken);

        if (task is null)
            return Result.Failure("Task not found.");

        var oldStatus = task.Status;

        // Source column
        var sourceTasks = await repository.GetOrderedTasksAsync(
            request.Request.ProjectId,
            oldStatus,
            cancellationToken);

        sourceTasks.RemoveAll(x => x.Id == task.Id);

        for (var i = 0; i < sourceTasks.Count; i++)
        {
            sourceTasks[i].Order = i;
        }

        // Destination column
        List<Domain.Entities.WorkTask> destinationTasks;

        if (oldStatus == request.Request.Status)
        {
            destinationTasks = sourceTasks;
        }
        else
        {
            destinationTasks = await repository.GetOrderedTasksAsync(
                request.Request.ProjectId,
                request.Request.Status,
                cancellationToken);
        }

        task.Status = request.Request.Status;

        var index = Math.Clamp(
            request.Request.Order,
            0,
            destinationTasks.Count);

        destinationTasks.Insert(index, task);

        for (var i = 0; i < destinationTasks.Count; i++)
        {
            destinationTasks[i].Order = i;
        }

        if (oldStatus != request.Request.Status)
        {
            await repository.UpdateRangeAsync(
                sourceTasks,
                cancellationToken);
        }

        await repository.UpdateRangeAsync(
            destinationTasks,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(
            cancellationToken);

        return Result.Success();
    }
}
