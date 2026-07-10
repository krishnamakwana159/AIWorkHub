using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Notifications.GetUnreadCount;

public sealed class GetUnreadNotificationCountQueryHandler(
    INotificationRepository repository,
    ICurrentUserService currentUserService)
    : IRequestHandler<GetUnreadNotificationCountQuery, Result<int>>
{
    public async Task<Result<int>> Handle(
        GetUnreadNotificationCountQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUserService.UserId, out var userId))
            return Result<int>.Failure("User not found.");

        var count =
            await repository.GetUnreadCountAsync(
                userId,
                cancellationToken);

        return Result<int>.Success(count);
    }
}
