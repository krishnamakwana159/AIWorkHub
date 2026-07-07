using AIWorkHub.SharedKernel.Entities;

namespace AIWorkHub.Domain.Entities;

public class TaskComment : AuditableEntity
{
    public Guid TaskId { get; set; }

    public WorkTask Task { get; set; } = null!;

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public string Comment { get; set; } = string.Empty;
}
