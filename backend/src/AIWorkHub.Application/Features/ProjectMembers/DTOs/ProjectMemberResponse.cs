using AIWorkHub.Domain.Enums;

namespace AIWorkHub.Application.Features.ProjectMembers.DTOs;

public sealed class ProjectMemberResponse
{
    public Guid UserId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public ProjectRole Role { get; set; }
}
