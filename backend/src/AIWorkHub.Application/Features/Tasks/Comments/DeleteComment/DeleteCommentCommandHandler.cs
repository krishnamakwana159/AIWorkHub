using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.Comments.DeleteComment;

public sealed class DeleteCommentCommandHandler(
    ITaskCommentRepository repository,
    IActivityService activityService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCommentCommand, Result>
{
    public async Task<Result> Handle(
        DeleteCommentCommand request,
        CancellationToken cancellationToken)
    {
        var comment = await repository.GetByIdAsync(
            request.CommentId,
            cancellationToken);

        if (comment is null)
            return Result.Failure("Comment not found.");

        repository.Remove(comment);

        await activityService.LogAsync(
            ActivityEntityType.Comment,
            comment.Id,
            ActivityAction.CommentDeleted,
            "Comment deleted.",
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
