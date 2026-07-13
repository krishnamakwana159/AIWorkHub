namespace AIWorkHub.Application.Features.Reports.DashboardAnalytics;
public sealed class TopPerformerDto
{
    public Guid UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public decimal ProductivityScore { get; set; }
}
