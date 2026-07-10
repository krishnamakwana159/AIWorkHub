using AIWorkHub.Application.Features.Tasks.Comments.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.Comments.UpdateComment;

public sealed record UpdateCommentCommand(
    Guid CommentId,
    UpdateCommentRequest Request)
    : IRequest<Result>;
