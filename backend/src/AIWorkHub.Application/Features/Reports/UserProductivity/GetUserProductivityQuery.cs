using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Reports.UserProductivity;

public sealed record GetUserProductivityQuery(Guid UserId)
    : IRequest<Result<UserProductivityResponse>>;
