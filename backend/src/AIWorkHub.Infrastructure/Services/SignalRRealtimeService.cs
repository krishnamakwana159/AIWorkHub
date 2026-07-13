using AIWorkHub.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;
using AIWorkHub.Infrastructure.Hubs;

namespace AIWorkHub.Infrastructure.Services;

public sealed class SignalRRealtimeService(
    IHubContext<NotificationHub> notificationHub,
    IHubContext<KanbanHub> kanbanHub)
    : IRealtimeService
{
    public async Task SendToUserAsync(
        Guid userId,
        string eventName,
        object payload,
        CancellationToken cancellationToken = default)
    {
        await notificationHub
            .Clients
            .User(userId.ToString())
            .SendAsync(eventName, payload, cancellationToken);
    }

    public async Task SendToProjectAsync(
        Guid projectId,
        string eventName,
        object payload,
        CancellationToken cancellationToken = default)
    {
        await kanbanHub
            .Clients
            .Group(projectId.ToString())
            .SendAsync(eventName, payload, cancellationToken);
    }

    public async Task BroadcastAsync(
        string eventName,
        object payload,
        CancellationToken cancellationToken = default)
    {
        await notificationHub
            .Clients
            .All
            .SendAsync(eventName, payload, cancellationToken);
    }
}
