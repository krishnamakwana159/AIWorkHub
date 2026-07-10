using AIWorkHub.Application.Features.Tasks.Comments.DTOs;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Results;
using AutoMapper;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.Comments.GetAllComment;

public sealed class GetCommentsQueryHandler(
    ITaskCommentRepository repository,
    IMapper mapper)
    : IRequestHandler<GetCommentsQuery,
        Result<IReadOnlyList<CommentResponse>>>
{
    public async Task<Result<IReadOnlyList<CommentResponse>>> Handle(
        GetCommentsQuery request,
        CancellationToken cancellationToken)
    {
        var comments = await repository.GetByTaskIdAsync(
            request.TaskId,
            cancellationToken);

        return Result<IReadOnlyList<CommentResponse>>.Success(
            mapper.Map<IReadOnlyList<CommentResponse>>(comments));
    }
}
