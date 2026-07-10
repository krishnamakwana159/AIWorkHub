using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Entities;

namespace AIWorkHub.Domain.Entities;

public sealed class ProjectMember : AuditableEntity
{
    public Guid ProjectId { get; set; }

    public Project Project { get; set; } = null!;

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public ProjectRole Role { get; set; } = ProjectRole.Member;
}
