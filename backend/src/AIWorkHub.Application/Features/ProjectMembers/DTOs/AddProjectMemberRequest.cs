using AIWorkHub.Domain.Enums;

namespace AIWorkHub.Application.Features.ProjectMembers.DTOs;

public sealed class AddProjectMemberRequest
{
    public Guid UserId { get; set; }

    public ProjectRole Role { get; set; } = ProjectRole.Member;
}
