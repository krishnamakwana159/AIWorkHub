namespace AIWorkHub.Application.Features.TimeTracking.AddManualEntry;
public sealed class TimeEntryResponse
{
    public Guid Id { get; set; }

    public Guid WorkTaskId { get; set; }

    public string TaskTitle { get; set; } = string.Empty;

    public DateTime StartTimeUtc { get; set; }

    public DateTime? EndTimeUtc { get; set; }

    public decimal Hours { get; set; }

    public bool IsRunning { get; set; }

    public string? Description { get; set; }

    public string UserName { get; set; } = string.Empty;
}
