namespace AIWorkHub.Application.Features.Reports.DashboardAnalytics;

public sealed class DashboardAnalyticsResponse
{
    public DateTime GeneratedAtUtc { get; set; } = DateTime.UtcNow;

    public KPIOverviewDto Overview { get; set; } = new();

    public List<MonthlyTrendDto> MonthlyTrend { get; set; } = [];

    public List<ProjectProgressDto> ProjectProgress { get; set; } = [];

    public List<TeamPerformanceDto> TeamPerformance { get; set; } = [];

    public List<TopPerformerDto> TopPerformers { get; set; } = [];
}
