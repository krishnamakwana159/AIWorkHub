namespace AIWorkHub.Application.Features.TimeTracking.AddManualEntry;

public sealed class AddManualEntryRequest
{
    public Guid WorkTaskId { get; set; }

    public DateTime StartTimeUtc { get; set; }

    public DateTime EndTimeUtc { get; set; }

    public string? Description { get; set; }
}
