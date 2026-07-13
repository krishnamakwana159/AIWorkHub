using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.TimeTracking.StopTimer;

public sealed class StopTimerCommandHandler(
    IWorkTimeEntryRepository timeEntryRepository,
    IWorkTaskRepository taskRepository,
    ICurrentUserService currentUserService,
    IActivityService activityService,
    IUnitOfWork unitOfWork,
    IRealtimeService realtimeService)
    : IRequestHandler<StopTimerCommand, Result>
{
    public async Task<Result> Handle(
        StopTimerCommand request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUserService.UserId, out var userId))
            return Result.Failure("Unauthorized.");

        var entry = await timeEntryRepository.GetRunningTimerAsync(
            userId,
            cancellationToken);

        if (entry is null)
            return Result.Failure("No running timer found.");

        entry.EndTimeUtc = DateTime.UtcNow;
        entry.IsRunning = false;

        var duration = entry.EndTimeUtc.Value - entry.StartTimeUtc;

        // entry.Hours = Convert.ToDecimal(duration.TotalHours);
        entry.Hours = Math.Round(
            Convert.ToDecimal(duration.TotalHours),
            2,
            MidpointRounding.AwayFromZero);

        var task = await taskRepository.GetByIdAsync(
            entry.WorkTaskId,
            cancellationToken);

        if (task is not null)
        {
            task.ActualHours += entry.Hours;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await realtimeService.SendToProjectAsync(
            task.ProjectId,
            "TimerStopped",
            new
            {
                task.Id,
                hours = entry.Hours
            },
            cancellationToken);

        if (task is not null)
        {
            await activityService.LogAsync(
                ActivityEntityType.Task,
                task.Id,
                ActivityAction.Updated,
                $"Stopped timer for '{task.Title}'. Logged {entry.Hours:F2} hours.",
                cancellationToken);
        }

        return Result.Success();
    }
}
