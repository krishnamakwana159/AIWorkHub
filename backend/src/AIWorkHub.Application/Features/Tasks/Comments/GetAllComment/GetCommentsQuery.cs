using AIWorkHub.Application.Features.Tasks.Comments.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.Comments.GetAllComment;

public sealed record GetCommentsQuery(Guid TaskId)
    : IRequest<Result<IReadOnlyList<CommentResponse>>>;
