using AIWorkHub.Domain.Enums;

namespace AIWorkHub.Application.Features.ProjectMembers.DTOs;

public sealed class UpdateProjectMemberRoleRequest
{
    public ProjectRole Role { get; set; }
}
