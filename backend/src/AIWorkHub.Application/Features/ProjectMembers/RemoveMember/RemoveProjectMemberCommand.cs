using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.ProjectMembers.RemoveMember;

public sealed record RemoveProjectMemberCommand(Guid ProjectId, Guid UserId)
    : IRequest<Result>;
