using AIWorkHub.Application.Features.Notifications.DTOs;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using AutoMapper;
using MediatR;

namespace AIWorkHub.Application.Features.Notifications.GetAll;

public sealed class GetNotificationsQueryHandler(
    INotificationRepository repository,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : IRequestHandler<
        GetNotificationsQuery,
        Result<IReadOnlyList<NotificationResponse>>>
{
    public async Task<Result<IReadOnlyList<NotificationResponse>>> Handle(
        GetNotificationsQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUserService.UserId, out var userId))
            return Result<IReadOnlyList<NotificationResponse>>
                .Failure("User not found.");

        var notifications =
            await repository.GetUserNotificationsAsync(
                userId,
                cancellationToken);

        return Result<IReadOnlyList<NotificationResponse>>
            .Success(
                mapper.Map<IReadOnlyList<NotificationResponse>>(notifications));
    }
}
