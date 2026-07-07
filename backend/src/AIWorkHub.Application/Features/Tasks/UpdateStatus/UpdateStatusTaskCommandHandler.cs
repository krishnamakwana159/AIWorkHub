using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.UpdateStatus;

public sealed class UpdateTaskStatusCommandHandler(
    IWorkTaskRepository taskRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateTaskStatusCommand, Result>
{
    public async Task<Result> Handle(
        UpdateTaskStatusCommand request,
        CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(
            request.TaskId,
            cancellationToken);

        if (task is null)
            return Result.Failure("Task not found.");

        if (task.Status == request.Status)
            return Result.Success();

        task.Status = request.Status;

        if (request.Status == Domain.Enums.TaskStatus.Completed)
        {
            task.CompletedAtUtc = DateTime.UtcNow;
        }
        else
        {
            task.CompletedAtUtc = null;
        }

        taskRepository.Update(task);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
