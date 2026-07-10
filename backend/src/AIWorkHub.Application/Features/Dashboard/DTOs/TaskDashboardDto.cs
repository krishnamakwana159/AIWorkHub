namespace AIWorkHub.Application.Features.Dashboard.DTOs;

public sealed class TaskDashboardDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public Guid ProjectId { get; set; }

    public string ProjectName { get; set; } = string.Empty;
}
