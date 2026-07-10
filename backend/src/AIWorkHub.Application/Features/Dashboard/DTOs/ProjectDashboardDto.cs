namespace AIWorkHub.Application.Features.Dashboard.DTOs;

public sealed class ProjectDashboardDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTimeOffset CreatedAtUtc { get; set; }
}
