namespace AIWorkHub.Application.Features.Reports.DashboardAnalytics;

public sealed class DashboardOverviewDto
{
    public int TotalProjects { get; set; }

    public int TotalTasks { get; set; }

    public int CompletedTasks { get; set; }

    public int ActiveTasks { get; set; }

    public int OverdueTasks { get; set; }

    public int TotalUsers { get; set; }

    public decimal EstimatedHours { get; set; }

    public decimal ActualHours { get; set; }

    public decimal CompletionPercentage { get; set; }
}
