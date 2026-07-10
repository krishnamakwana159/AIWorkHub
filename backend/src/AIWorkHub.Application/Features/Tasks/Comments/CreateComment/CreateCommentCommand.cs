using AIWorkHub.Application.Features.Tasks.Comments.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.Comments.CreateComment;

public sealed record CreateCommentCommand(
    Guid TaskId,
    CreateCommentRequest Request)
    : IRequest<Result<CommentResponse>>;
