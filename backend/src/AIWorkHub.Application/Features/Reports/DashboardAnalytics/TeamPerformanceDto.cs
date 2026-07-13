namespace AIWorkHub.Application.Features.Reports.DashboardAnalytics;
public sealed class TeamPerformanceDto
{
    public Guid UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public int AssignedTasks { get; set; }

    public int CompletedTasks { get; set; }

    public decimal CompletionRate { get; set; }

    public decimal HoursLogged { get; set; }
}
