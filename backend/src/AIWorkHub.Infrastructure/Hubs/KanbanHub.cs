using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace AIWorkHub.Infrastructure.Hubs;

[Authorize]
public sealed class KanbanHub : Hub
{
    public async Task JoinProject(Guid projectId)
    {
        await Groups.AddToGroupAsync(
            Context.ConnectionId,
            projectId.ToString());
    }

    public async Task LeaveProject(Guid projectId)
    {
        await Groups.RemoveFromGroupAsync(
            Context.ConnectionId,
            projectId.ToString());
    }
}
