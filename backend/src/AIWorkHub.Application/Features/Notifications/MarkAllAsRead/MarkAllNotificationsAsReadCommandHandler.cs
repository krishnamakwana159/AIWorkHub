using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Notifications.MarkAllAsRead;

public sealed class MarkAllNotificationsAsReadCommandHandler(
    INotificationRepository repository,
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<MarkAllNotificationsAsReadCommand, Result>
{
    public async Task<Result> Handle(
        MarkAllNotificationsAsReadCommand request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUserService.UserId, out var userId))
            return Result.Failure("User not found.");

        await repository.MarkAllReadAsync(
            userId,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
