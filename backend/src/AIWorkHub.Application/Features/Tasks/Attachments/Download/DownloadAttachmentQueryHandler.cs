using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.Attachments.Download;

public sealed class DownloadAttachmentQueryHandler(
    ITaskAttachmentRepository repository,
    IFileStorageService storage)
    : IRequestHandler<DownloadAttachmentQuery, Result<DownloadAttachmentResponse>>
{
    public async Task<Result<DownloadAttachmentResponse>> Handle(
        DownloadAttachmentQuery request,
        CancellationToken cancellationToken)
    {
        var attachment = await repository.GetByIdWithUserAsync(
            request.AttachmentId,
            cancellationToken);

        if (attachment is null)
            return Result<DownloadAttachmentResponse>.Failure("Attachment not found.");

        var stream = await storage.OpenReadAsync(
            attachment.FilePath,
            cancellationToken);

        return Result<DownloadAttachmentResponse>.Success(
            new DownloadAttachmentResponse
            {
                Stream = stream,
                FileName = attachment.FileName,
                ContentType = attachment.ContentType
            });
    }
}
