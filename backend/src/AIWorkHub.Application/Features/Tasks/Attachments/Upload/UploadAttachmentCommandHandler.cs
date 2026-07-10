using AIWorkHub.Application.Features.Tasks.Attachments.DTOs;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using AutoMapper;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.Attachments.Upload;

public sealed class UploadAttachmentCommandHandler(
    IWorkTaskRepository taskRepository,
    ITaskAttachmentRepository attachmentRepository,
    IFileStorageService fileStorageService,
    ICurrentUserService currentUserService,
    IActivityService activityService,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UploadAttachmentCommand, Result<AttachmentResponse>>
{
    public async Task<Result<AttachmentResponse>> Handle(
        UploadAttachmentCommand request,
        CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(
            request.TaskId,
            cancellationToken);

        if (task is null)
            return Result<AttachmentResponse>.Failure("Task not found.");

        if (!Guid.TryParse(currentUserService.UserId, out var userId))
            return Result<AttachmentResponse>.Failure("User not found.");

        var (storedFileName, filePath) =
            await fileStorageService.SaveAsync(
                request.File,
                cancellationToken);

        var attachment = new TaskAttachment
        {
            TaskId = task.Id,
            FileName = request.File.FileName,
            StoredFileName = storedFileName,
            FilePath = filePath,
            FileSize = request.File.Length,
            ContentType = request.File.ContentType,
            UploadedBy = userId
        };

        await attachmentRepository.AddAsync(
            attachment,
            cancellationToken);

        await activityService.LogAsync(
            ActivityEntityType.Task,
            task.Id,
            ActivityAction.AttachmentUploaded,
            $"Attachment '{attachment.FileName}' uploaded.",
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var savedAttachment =
            await attachmentRepository.GetByIdWithUserAsync(
                attachment.Id,
                cancellationToken);

        return Result<AttachmentResponse>.Success(
            mapper.Map<AttachmentResponse>(savedAttachment!));
    }
}
