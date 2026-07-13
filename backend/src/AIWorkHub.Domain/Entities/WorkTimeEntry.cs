using AIWorkHub.SharedKernel.Entities;

namespace AIWorkHub.Domain.Entities;

public sealed class WorkTimeEntry : SoftDeleteEntity
{
    public Guid WorkTaskId { get; set; }

    public WorkTask WorkTask { get; set; } = null!;

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public DateTime StartTimeUtc { get; set; }

    public DateTime? EndTimeUtc { get; set; }

    public decimal Hours { get; set; }

    public string? Description { get; set; }

    public bool IsRunning { get; set; }

}
