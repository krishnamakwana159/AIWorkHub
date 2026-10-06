using AIWorkHub.Application.Features.TimeTracking.AddManualEntry;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.TimeTracking.GetTaskEntries;

public sealed class GetTaskTimeEntriesQueryHandler(
    IWorkTimeEntryRepository repository,
    IWorkTaskRepository taskRepository)
    : IRequestHandler<GetTaskTimeEntriesQuery, Result<List<TimeEntryResponse>>>
{
    public async Task<Result<List<TimeEntryResponse>>> Handle(
        GetTaskTimeEntriesQuery request,
        CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(
            request.WorkTaskId,
            cancellationToken);

        if (task is null)
            return Result<List<TimeEntryResponse>>.Failure("Task not found.");

        var entries = await repository.GetTaskEntriesAsync(
            request.WorkTaskId,
            cancellationToken);

        var response = entries.Select(x => new TimeEntryResponse
        {
            Id = x.Id,
            WorkTaskId = x.WorkTaskId,
            TaskTitle = task.Title,
            StartTimeUtc = x.StartTimeUtc,
            EndTimeUtc = x.EndTimeUtc,
            Hours = x.Hours,
            IsRunning = x.IsRunning,
            Description = x.Description,
            UserName = x.User == null
                ? string.Empty
                : $"{x.User.FirstName} {x.User.LastName}".Trim()
        }).ToList();

        return Result<List<TimeEntryResponse>>.Success(response);
    }
}
