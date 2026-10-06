using AIWorkHub.SharedKernel.Entities;

namespace AIWorkHub.Domain.Entities;

public sealed class TaskTag : BaseEntity
{
    public Guid WorkTaskId { get; set; }

    public WorkTask WorkTask { get; set; } = null!;

    public Guid TagId { get; set; }

    public Tag Tag { get; set; } = null!;
}
