using AIWorkHub.Application.Features.Tasks.Attachments.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.Attachments.GetAll;

public sealed record GetTaskAttachmentsQuery(Guid TaskId)
    : IRequest<Result<IReadOnlyList<AttachmentResponse>>>;
