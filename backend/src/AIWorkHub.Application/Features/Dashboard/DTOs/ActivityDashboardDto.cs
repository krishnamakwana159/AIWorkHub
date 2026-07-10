namespace AIWorkHub.Application.Features.Dashboard.DTOs;

public sealed class ActivityDashboardDto
{
    public Guid Id { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;

    public DateTimeOffset CreatedAtUtc { get; set; }
}
