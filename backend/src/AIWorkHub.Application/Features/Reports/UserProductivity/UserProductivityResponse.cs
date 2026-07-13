namespace AIWorkHub.Application.Features.Reports.UserProductivity;

public sealed class UserProductivityResponse
{
    public Guid UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public int AssignedTasks { get; set; }

    public int CompletedTasks { get; set; }

    public int InProgressTasks { get; set; }

    public int TodoTasks { get; set; }

    public int ReviewTasks { get; set; }

    public int OverdueTasks { get; set; }

    public decimal EstimatedHours { get; set; }

    public decimal ActualHours { get; set; }

    public decimal CompletionRate { get; set; }

    public decimal ProductivityScore { get; set; }
}
