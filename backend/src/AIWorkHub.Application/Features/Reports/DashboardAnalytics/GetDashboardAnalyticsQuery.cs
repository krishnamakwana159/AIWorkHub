using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Reports.DashboardAnalytics;

public sealed record GetDashboardAnalyticsQuery()
    : IRequest<Result<DashboardAnalyticsResponse>>;
