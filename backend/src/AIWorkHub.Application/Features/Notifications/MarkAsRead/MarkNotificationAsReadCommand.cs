using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Notifications.MarkAsRead;

public sealed record MarkNotificationAsReadCommand(Guid NotificationId)
    : IRequest<Result>;
