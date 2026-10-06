using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tags.RemoveTagFromTask;

public sealed class RemoveTagFromTaskCommandHandler(
    ITaskTagRepository taskTagRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RemoveTagFromTaskCommand, Result>
{
    public async Task<Result> Handle(
        RemoveTagFromTaskCommand request,
        CancellationToken cancellationToken)
    {
        var taskTag = await taskTagRepository.GetAsync(
            request.TaskId,
            request.TagId,
            cancellationToken);

        if (taskTag is null)
            return Result.Success();

        taskTagRepository.Remove(taskTag);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
