using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.Attachments.Delete;

public sealed record DeleteAttachmentCommand(Guid AttachmentId)
    : IRequest<Result>;
