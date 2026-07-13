using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.TimeTracking.StartTimer;

public sealed class StartTimerCommandHandler(
    IWorkTimeEntryRepository repository,
    IWorkTaskRepository taskRepository,
    ICurrentUserService currentUser,
    IActivityService activityService,
    IUnitOfWork unitOfWork,
    IRealtimeService realtimeService)
    : IRequestHandler<StartTimerCommand, Result>
{
    public async Task<Result> Handle(
        StartTimerCommand request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUser.UserId, out var userId))
            return Result.Failure("Unauthorized.");

        var task = await taskRepository.GetByIdAsync(
            request.Request.WorkTaskId,
            cancellationToken);

        if (task is null)
            return Result.Failure("Task not found.");

        var runningTimer = await repository.GetRunningTimerAsync(
            userId,
            cancellationToken);

        if (runningTimer is not null)
            return Result.Failure(
                "You already have a running timer.");

        var entry = new WorkTimeEntry
        {
            WorkTaskId = task.Id,
            UserId = userId,
            StartTimeUtc = DateTime.UtcNow,
            IsRunning = true
        };

        await repository.AddAsync(entry, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await realtimeService.SendToProjectAsync(
            task.ProjectId,
            "TimerStarted",
            new
            {
                task.Id,
                userId
            },
            cancellationToken);

        await activityService.LogAsync(
            ActivityEntityType.Task,
            task.Id,
            ActivityAction.Updated,
            $"Started timer for '{task.Title}'.",
            cancellationToken);

        return Result.Success();
    }
}
