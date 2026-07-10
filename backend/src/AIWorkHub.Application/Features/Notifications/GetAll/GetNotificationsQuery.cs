using AIWorkHub.Application.Features.Notifications.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Notifications.GetAll;

public sealed record GetNotificationsQuery()
    : IRequest<Result<IReadOnlyList<NotificationResponse>>>;
