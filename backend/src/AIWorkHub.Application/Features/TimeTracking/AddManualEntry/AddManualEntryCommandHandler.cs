using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.TimeTracking.AddManualEntry;

public sealed class AddManualEntryCommandHandler(
    IWorkTaskRepository taskRepository,
    IWorkTimeEntryRepository repository,
    ICurrentUserService currentUser,
    IActivityService activityService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AddManualEntryCommand, Result>
{
    public async Task<Result> Handle(
        AddManualEntryCommand request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUser.UserId, out var userId))
            return Result.Failure("Unauthorized.");

        var task = await taskRepository.GetByIdAsync(
            request.Request.WorkTaskId,
            cancellationToken);

        if (task is null)
            return Result.Failure("Task not found.");

        var hours = Math.Round(
            Convert.ToDecimal(
                (request.Request.EndTimeUtc -
                 request.Request.StartTimeUtc).TotalHours),
            2,
            MidpointRounding.AwayFromZero);

        var entry = new WorkTimeEntry
        {
            WorkTaskId = task.Id,
            UserId = userId,
            StartTimeUtc = request.Request.StartTimeUtc,
            EndTimeUtc = request.Request.EndTimeUtc,
            Hours = hours,
            Description = request.Request.Description,
            IsRunning = false
        };

        await repository.AddAsync(entry, cancellationToken);

        task.ActualHours += hours;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await activityService.LogAsync(
            ActivityEntityType.Task,
            task.Id,
            ActivityAction.Updated,
            $"Added manual time entry ({hours:F2} hrs).",
            cancellationToken);

        return Result.Success();
    }
}
