using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Entities;
using TaskStatus = AIWorkHub.Domain.Enums.TaskStatus;

namespace AIWorkHub.Domain.Entities;
public sealed class WorkTask : SoftDeleteEntity
{
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public TaskStatus Status { get; set; } = TaskStatus.Todo;

    public TaskPriority Priority { get; set; } = TaskPriority.Medium;

    public DateTime? StartDateUtc { get; set; }

    public DateTime? DueDateUtc { get; set; }

    public DateTime? CompletedAtUtc { get; set; }

    public decimal EstimatedHours { get; set; }

    public decimal ActualHours { get; set; }

    public int Order { get; set; }

    public bool IsPinned { get; set; }

    public bool IsFavorite { get; set; }

    public Guid ProjectId { get; set; }

    public Project Project { get; set; } = null!;

    public Guid? AssignedUserId { get; set; }

    public User? AssignedUser { get; set; }
    public ICollection<TaskComment> Comments { get; set; } = new List<TaskComment>();
}
