using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Notifications.Delete;

public sealed record DeleteNotificationCommand(Guid NotificationId)
    : IRequest<Result>;
