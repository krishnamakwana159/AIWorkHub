namespace AIWorkHub.Application.Features.Reports.ProjectReport;
public sealed class ProjectReportResponse
{
    public Guid ProjectId { get; set; }

    public string ProjectName { get; set; } = string.Empty;

    public int TotalTasks { get; set; }

    public int TodoTasks { get; set; }

    public int InProgressTasks { get; set; }

    public int ReviewTasks { get; set; }

    public int DoneTasks { get; set; }

    public decimal CompletionPercentage { get; set; }

    public decimal EstimatedHours { get; set; }

    public decimal ActualHours { get; set; }

    public int OverdueTasks { get; set; }
}
