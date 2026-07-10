using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Notifications.MarkAllAsRead;

public sealed record MarkAllNotificationsAsReadCommand()
    : IRequest<Result>;
