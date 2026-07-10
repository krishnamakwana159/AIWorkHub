using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Entities;

namespace AIWorkHub.Domain.Entities;

public sealed class Project : SoftDeleteEntity
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string Color { get; set; } = "#2563EB";

    public ProjectStatus Status { get; set; }

    public ProjectPriority Priority { get; set; }

    public bool IsArchived { get; set; }

    public bool IsFavorite { get; set; }

    public DateTime? StartDateUtc { get; set; }

    public DateTime? TargetCompletionDateUtc { get; set; }

    public DateTime? CompletedAtUtc { get; set; }

    public decimal Progress { get; set; }

    public Guid OwnerId { get; set; }

    public User Owner { get; set; } = null!;

    public ICollection<WorkTask> Tasks { get; set; } = new List<WorkTask>();
    public ICollection<ProjectMember> Members { get; set; } = new List<ProjectMember>();

}
