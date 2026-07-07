namespace AIWorkHub.Application.Features.Projects.DTOs;

public sealed class ProjectDashboardResponse
{
    public Guid ProjectId { get; set; }

    public int TotalTasks { get; set; }

    public int CompletedTasks { get; set; }

    public int PendingTasks { get; set; }

    public int OverdueTasks { get; set; }

    public decimal Progress { get; set; }
}
