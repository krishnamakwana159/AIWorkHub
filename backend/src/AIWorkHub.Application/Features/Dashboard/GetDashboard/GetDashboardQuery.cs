using AIWorkHub.Application.Features.Dashboard.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Dashboard.GetDashboard;

public sealed record GetDashboardQuery()
    : IRequest<Result<DashboardResponse>>;
