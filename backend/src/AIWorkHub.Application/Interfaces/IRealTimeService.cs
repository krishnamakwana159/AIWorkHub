namespace AIWorkHub.Application.Interfaces;

public interface IRealtimeService
{
    Task SendToUserAsync(
        Guid userId,
        string eventName,
        object payload,
        CancellationToken cancellationToken = default);

    Task SendToProjectAsync(
        Guid projectId,
        string eventName,
        object payload,
        CancellationToken cancellationToken = default);

    Task BroadcastAsync(
        string eventName,
        object payload,
        CancellationToken cancellationToken = default);
}
