using AIWorkHub.Application.Features.ProjectMembers.DTOs;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Results;
using AutoMapper;
using MediatR;

namespace AIWorkHub.Application.Features.ProjectMembers.GetMembers;

public sealed class GetProjectMembersQueryHandler(
    IProjectMemberRepository repository,
    IMapper mapper)
    : IRequestHandler<
        GetProjectMembersQuery,
        Result<IReadOnlyList<ProjectMemberResponse>>>
{
    public async Task<Result<IReadOnlyList<ProjectMemberResponse>>> Handle(
        GetProjectMembersQuery request,
        CancellationToken cancellationToken)
    {
        var members = await repository.GetProjectMembersAsync(
            request.ProjectId,
            cancellationToken);

        return Result<IReadOnlyList<ProjectMemberResponse>>
            .Success(
                mapper.Map<IReadOnlyList<ProjectMemberResponse>>(members));
    }
}
