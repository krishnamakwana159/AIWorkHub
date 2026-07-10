using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Notifications.Delete;

public sealed class DeleteNotificationCommandHandler(
    INotificationRepository repository,
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteNotificationCommand, Result>
{
    public async Task<Result> Handle(
        DeleteNotificationCommand request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUserService.UserId, out var userId))
            return Result.Failure("User not found.");

        var notification = await repository.GetByIdAsync(
            request.NotificationId,
            cancellationToken);

        if (notification is null || notification.UserId != userId)
            return Result.Failure("Notification not found.");

        repository.Remove(notification);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
