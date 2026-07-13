namespace AIWorkHub.Application.Features.Reports.DashboardAnalytics;
public sealed class MonthlyTrendDto
{
    public int Year { get; set; }

    public int Month { get; set; }

    public int TasksCreated { get; set; }

    public int TasksCompleted { get; set; }

    public decimal HoursLogged { get; set; }
}
