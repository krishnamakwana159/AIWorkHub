using AIWorkHub.Domain.Enums;

namespace AIWorkHub.Application.Features.Reports.DashboardAnalytics;

public sealed class DashboardTaskStatusDto
{
    public WorkTaskStatus Status { get; set; }

    public int Count { get; set; }
}
