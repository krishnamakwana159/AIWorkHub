using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.Attachments.Delete;

public sealed class DeleteAttachmentCommandHandler(
    ITaskAttachmentRepository repository,
    IFileStorageService storage,
    IActivityService activityService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteAttachmentCommand, Result>
{
    public async Task<Result> Handle(
        DeleteAttachmentCommand request,
        CancellationToken cancellationToken)
    {
        var attachment = await repository.GetByIdWithUserAsync(
            request.AttachmentId,
            cancellationToken);

        if (attachment is null)
            return Result.Failure("Attachment not found.");

        await storage.DeleteAsync(
            attachment.FilePath,
            cancellationToken);

        repository.Remove(attachment);

        await activityService.LogAsync(
            ActivityEntityType.Task,
            attachment.TaskId,
            ActivityAction.AttachmentDeleted,
            $"Attachment '{attachment.FileName}' deleted.",
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
