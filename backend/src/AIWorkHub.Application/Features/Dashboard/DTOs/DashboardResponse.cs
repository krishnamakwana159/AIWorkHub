namespace AIWorkHub.Application.Features.Dashboard.DTOs;

public sealed class DashboardResponse
{
    public int TotalProjects { get; set; }

    public int ActiveProjects { get; set; }

    public int TotalTasks { get; set; }

    public int TodoTasks { get; set; }

    public int InProgressTasks { get; set; }

    public int CompletedTasks { get; set; }

    public int OverdueTasks { get; set; }

    public int MyPendingTasks { get; set; }

    public int UnreadNotifications { get; set; }

    public IReadOnlyList<ProjectDashboardDto> RecentProjects { get; set; }
        = [];

    public IReadOnlyList<TaskDashboardDto> RecentTasks { get; set; }
        = [];

    public IReadOnlyList<ActivityDashboardDto> RecentActivities { get; set; }
        = [];
}
