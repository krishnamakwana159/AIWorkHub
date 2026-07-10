using AIWorkHub.Application.Common.Models;
using AIWorkHub.Application.Features.Tasks.Attachments.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.Attachments.Upload;

public sealed record UploadAttachmentCommand(
    Guid TaskId,
    FileUploadDto File)
    : IRequest<Result<AttachmentResponse>>;
