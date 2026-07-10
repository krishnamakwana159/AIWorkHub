using AIWorkHub.Application.Features.Dashboard.DTOs;

namespace AIWorkHub.Application.Interfaces.Repositories;

public interface IDashboardRepository
{
    Task<DashboardResponse> GetDashboardAsync(
        Guid userId,
        CancellationToken cancellationToken);
}
