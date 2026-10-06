using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Features.TimeTracking.AddManualEntry;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.TimeTracking.GetRunningTimer;

public sealed class GetRunningTimerQueryHandler(
    IWorkTimeEntryRepository repository,
    ICurrentUserService currentUser)
    : IRequestHandler<GetRunningTimerQuery, Result<TimeEntryResponse?>>
{
    public async Task<Result<TimeEntryResponse?>> Handle(
        GetRunningTimerQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUser.UserId, out var userId))
            return Result<TimeEntryResponse?>.Failure("Unauthorized.");

        var entry = await repository.GetRunningTimerAsync(
            userId,
            cancellationToken);

        if (entry is null)
            return Result<TimeEntryResponse?>.Success(null);

        return Result<TimeEntryResponse?>.Success(new TimeEntryResponse
        {
            Id = entry.Id,
            WorkTaskId = entry.WorkTaskId,
            TaskTitle = entry.WorkTask?.Title ?? string.Empty,
            StartTimeUtc = entry.StartTimeUtc,
            EndTimeUtc = entry.EndTimeUtc,
            Hours = entry.Hours,
            IsRunning = entry.IsRunning,
            Description = entry.Description,
            UserName = entry.User == null
                ? string.Empty
                : $"{entry.User.FirstName} {entry.User.LastName}".Trim()
        });
    }
}
