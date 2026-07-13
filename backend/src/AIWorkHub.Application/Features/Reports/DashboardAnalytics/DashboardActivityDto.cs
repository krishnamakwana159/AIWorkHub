namespace AIWorkHub.Application.Features.Reports.DashboardAnalytics;

public sealed class DashboardActivityDto
{
    public Guid Id { get; set; }

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; }
}
