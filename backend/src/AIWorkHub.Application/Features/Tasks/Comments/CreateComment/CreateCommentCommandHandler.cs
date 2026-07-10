using AIWorkHub.Application.Features.Tasks.Comments.DTOs;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using AutoMapper;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.Comments.CreateComment;

public sealed class CreateCommentCommandHandler(
    ITaskCommentRepository commentRepository,
    IWorkTaskRepository taskRepository,
    ICurrentUserService currentUserService,
    IActivityService activityService,
    IUnitOfWork unitOfWork,
    INotificationService notificationService,
    IMapper mapper)
    : IRequestHandler<CreateCommentCommand, Result<CommentResponse>>
{
    public async Task<Result<CommentResponse>> Handle(
        CreateCommentCommand request,
        CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(
            request.TaskId,
            cancellationToken);

        if (task is null)
            return Result<CommentResponse>.Failure("Task not found.");

        if (!Guid.TryParse(currentUserService.UserId, out var userId))
            return Result<CommentResponse>.Failure("User not found.");

        var comment = new TaskComment
        {
            TaskId = request.TaskId,
            UserId = userId,
            Comment = request.Request.Comment
        };

        await commentRepository.AddAsync(comment, cancellationToken);

        await activityService.LogAsync(
            ActivityEntityType.Comment,
            comment.Id,
            ActivityAction.CommentAdded,
            $"Comment added to task '{task.Title}'.",
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var savedComment = await commentRepository.GetByIdWithUserAsync(
            comment.Id,
            cancellationToken);

        if (task.AssignedUserId.HasValue &&
            task.AssignedUserId.Value != userId)
        {
            await notificationService.NotifyAsync(
                task.AssignedUserId.Value,
                "New Comment",
                $"A new comment was added to '{task.Title}'.",
                NotificationType.CommentAdded,
                $"/tasks/{task.Id}",
                cancellationToken);
        }

        if (savedComment is null)
        {
            return Result<CommentResponse>.Failure(
                "Unable to load the created comment.");
        }

        return Result<CommentResponse>.Success(
            mapper.Map<CommentResponse>(savedComment));
    }
}
