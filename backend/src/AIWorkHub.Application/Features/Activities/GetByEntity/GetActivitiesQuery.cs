using AIWorkHub.Application.Features.Activities.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Activities.GetByEntity;

public sealed record GetActivitiesQuery(Guid EntityId)
    : IRequest<Result<IReadOnlyList<ActivityResponse>>>;
