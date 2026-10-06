using AIWorkHub.Application.Features.TimeTracking.AddManualEntry;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.TimeTracking.GetTaskEntries;

public sealed record GetTaskTimeEntriesQuery(Guid WorkTaskId)
    : IRequest<Result<List<TimeEntryResponse>>>;
