using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tags.AddTagToTask;

public sealed class AddTagToTaskCommandHandler(
    IWorkTaskRepository taskRepository,
    ITagRepository tagRepository,
    ITaskTagRepository taskTagRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AddTagToTaskCommand, Result>
{
    public async Task<Result> Handle(
        AddTagToTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(
            request.TaskId,
            cancellationToken);

        if (task is null)
            return Result.Failure("Task not found.");

        var tag = await tagRepository.GetByIdAsync(
            request.TagId,
            cancellationToken);

        if (tag is null)
            return Result.Failure("Tag not found.");

        if (tag.ProjectId != task.ProjectId)
            return Result.Failure("This tag does not belong to the task's project.");

        var existing = await taskTagRepository.GetAsync(
            request.TaskId,
            request.TagId,
            cancellationToken);

        if (existing is not null)
            return Result.Success();

        await taskTagRepository.AddAsync(
            new TaskTag
            {
                WorkTaskId = request.TaskId,
                TagId = request.TagId
            },
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
