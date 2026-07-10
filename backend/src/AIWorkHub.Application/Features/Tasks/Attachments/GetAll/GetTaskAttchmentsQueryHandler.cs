using AIWorkHub.Application.Features.Tasks.Attachments.DTOs;
using AIWorkHub.Application.Features.Tasks.Attachments.GetAll;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Results;
using AutoMapper;
using MediatR;

public sealed class GetTaskAttachmentsQueryHandler(
    ITaskAttachmentRepository repository,
    IMapper mapper)
    : IRequestHandler<GetTaskAttachmentsQuery,
        Result<IReadOnlyList<AttachmentResponse>>>
{
    public async Task<Result<IReadOnlyList<AttachmentResponse>>> Handle(
        GetTaskAttachmentsQuery request,
        CancellationToken cancellationToken)
    {
        var attachments = await repository.GetByTaskIdAsync(
            request.TaskId,
            cancellationToken);

        return Result<IReadOnlyList<AttachmentResponse>>.Success(
            mapper.Map<IReadOnlyList<AttachmentResponse>>(attachments));
    }
}
