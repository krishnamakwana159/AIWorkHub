using AIWorkHub.Application.Features.Dashboard.DTOs;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Dashboard.GetDashboard;

public sealed class GetDashboardQueryHandler(
    IDashboardRepository repository,
    ICurrentUserService currentUserService)
    : IRequestHandler<GetDashboardQuery, Result<DashboardResponse>>
{
    public async Task<Result<DashboardResponse>> Handle(
        GetDashboardQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUserService.UserId, out var userId))
            return Result<DashboardResponse>.Failure("User not found.");

        var dashboard = await repository.GetDashboardAsync(
            userId,
            cancellationToken);

        return Result<DashboardResponse>.Success(dashboard);
    }
}
