namespace AIWorkHub.Application.Features.Reports.DashboardAnalytics;
public sealed class ProjectProgressDto
{
    public Guid ProjectId { get; set; }

    public string ProjectName { get; set; } = string.Empty;

    public decimal Progress { get; set; }

    public int TotalTasks { get; set; }

    public int CompletedTasks { get; set; }

    public decimal EstimatedHours { get; set; }

    public decimal ActualHours { get; set; }
}
