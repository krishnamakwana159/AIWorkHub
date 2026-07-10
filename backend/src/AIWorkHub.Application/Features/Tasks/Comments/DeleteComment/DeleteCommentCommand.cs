using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.Comments.DeleteComment;

public sealed record DeleteCommentCommand(Guid CommentId)
    : IRequest<Result>;
