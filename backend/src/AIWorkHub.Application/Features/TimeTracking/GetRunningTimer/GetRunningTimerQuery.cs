using AIWorkHub.Application.Features.TimeTracking.AddManualEntry;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.TimeTracking.GetRunningTimer;

public sealed record GetRunningTimerQuery
    : IRequest<Result<TimeEntryResponse?>>;
