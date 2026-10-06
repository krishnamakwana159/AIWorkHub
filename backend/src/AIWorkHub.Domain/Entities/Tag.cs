using AIWorkHub.SharedKernel.Entities;

namespace AIWorkHub.Domain.Entities;

public sealed class Tag : AuditableEntity
{
    public string Name { get; set; } = string.Empty;

    public string Color { get; set; } = "#64748b";

    public Guid ProjectId { get; set; }

    public Project Project { get; set; } = null!;

    public ICollection<TaskTag> TaskTags { get; set; } = new List<TaskTag>();
}
