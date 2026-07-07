using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.DeleteTask;

public sealed class DeleteTaskCommandHandler(
    IWorkTaskRepository repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteTaskCommand, Result>
{
    public async Task<Result> Handle(
        DeleteTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (task is null)
            return Result.Failure("Task not found.");

        repository.Remove(task);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
