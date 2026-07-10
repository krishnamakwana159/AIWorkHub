using AIWorkHub.Application.Features.ProjectMembers.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.ProjectMembers.AddMember;

public sealed record AddProjectMemberCommand(
    Guid ProjectId,
    AddProjectMemberRequest Request)
    : IRequest<Result>;
