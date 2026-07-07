using AIWorkHub.Application.Features.Activities.DTOs;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Results;
using AutoMapper;
using MediatR;

namespace AIWorkHub.Application.Features.Activities.GetByEntity;

public sealed class GetActivitiesQueryHandler(
    IActivityLogRepository repository,
    IMapper mapper)
    : IRequestHandler<GetActivitiesQuery,
        Result<IReadOnlyList<ActivityResponse>>>
{
    public async Task<Result<IReadOnlyList<ActivityResponse>>> Handle(
        GetActivitiesQuery request,
        CancellationToken cancellationToken)
    {
        var activities =
            await repository.GetEntityActivitiesAsync(
                request.EntityId,
                cancellationToken);

        return Result<IReadOnlyList<ActivityResponse>>
            .Success(mapper.Map<IReadOnlyList<ActivityResponse>>(activities));
    }
}
