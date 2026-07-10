using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Notifications.GetUnreadCount;

public sealed record GetUnreadNotificationCountQuery()
    : IRequest<Result<int>>;
