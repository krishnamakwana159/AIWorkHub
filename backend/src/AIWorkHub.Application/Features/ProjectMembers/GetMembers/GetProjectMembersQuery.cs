using AIWorkHub.Application.Features.ProjectMembers.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.ProjectMembers.GetMembers;

public sealed record GetProjectMembersQuery(Guid ProjectId)
    : IRequest<Result<IReadOnlyList<ProjectMemberResponse>>>;
