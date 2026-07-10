using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.Comments.UpdateComment;

public sealed class UpdateCommentCommandHandler(
    ITaskCommentRepository repository,
    IActivityService activityService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCommentCommand, Result>
{
    public async Task<Result> Handle(
        UpdateCommentCommand request,
        CancellationToken cancellationToken)
    {
        var comment = await repository.GetByIdAsync(
            request.CommentId,
            cancellationToken);

        if (comment is null)
            return Result.Failure("Comment not found.");

        comment.Comment = request.Request.Comment;

        repository.Update(comment);

        await activityService.LogAsync(
            ActivityEntityType.Comment,
            comment.Id,
            ActivityAction.CommentUpdated,
            "Comment updated.",
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
