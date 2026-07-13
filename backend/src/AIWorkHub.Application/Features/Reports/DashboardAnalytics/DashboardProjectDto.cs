namespace AIWorkHub.Application.Features.Reports.DashboardAnalytics;

public sealed class DashboardProjectDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Progress { get; set; }

    public int TotalTasks { get; set; }

    public int CompletedTasks { get; set; }
}
