using AIWorkHub.Application.Features.ProjectMembers.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.ProjectMembers.UpdateMemberRole;

public sealed record UpdateProjectMemberRoleCommand(
    Guid ProjectId,
    Guid UserId,
    UpdateProjectMemberRoleRequest Request)
    : IRequest<Result>;
