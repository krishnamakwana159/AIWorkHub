using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.Attachments.Download;

public sealed record DownloadAttachmentQuery(Guid AttachmentId)
    : IRequest<Result<DownloadAttachmentResponse>>;
