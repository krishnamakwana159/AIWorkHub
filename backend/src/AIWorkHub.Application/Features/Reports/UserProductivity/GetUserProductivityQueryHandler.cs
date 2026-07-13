using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Reports.UserProductivity;

public sealed class GetUserProductivityQueryHandler(
    IUserRepository repository)
    : IRequestHandler<
        GetUserProductivityQuery,
        Result<UserProductivityResponse>>
{
    public async Task<Result<UserProductivityResponse>> Handle(
        GetUserProductivityQuery request,
        CancellationToken cancellationToken)
    {
        var user = await repository.GetUserWithTasksAsync(
            request.UserId,
            cancellationToken);

        if (user is null)
            return Result<UserProductivityResponse>
                .Failure("User not found.");

        var tasks = user.AssignedTasks;

        var completed = tasks.Count(x =>
            x.Status == WorkTaskStatus.Done);

        var assigned = tasks.Count;

        var completionRate =
            assigned == 0
                ? 0
                : Math.Round(
                    completed * 100m / assigned,
                    2);

        var estimatedHours = tasks.Sum(x =>
            x.EstimatedHours);

        var actualHours = tasks.Sum(x =>
            x.ActualHours);

        decimal productivityScore;

        if (estimatedHours == 0)
        {
            productivityScore = completionRate;
        }
        else
        {
            productivityScore = Math.Round(
                (completionRate * estimatedHours)
                / Math.Max(actualHours, 1),
                2);
        }

        var response = new UserProductivityResponse
        {
            UserId = user.Id,

            UserName =
                $"{user.FirstName} {user.LastName}",

            AssignedTasks = assigned,

            CompletedTasks = completed,

            TodoTasks = tasks.Count(x =>
                x.Status == WorkTaskStatus.Todo),

            InProgressTasks = tasks.Count(x =>
                x.Status == WorkTaskStatus.InProgress),

            ReviewTasks = tasks.Count(x =>
                x.Status == WorkTaskStatus.Review),

            OverdueTasks = tasks.Count(x =>
                x.DueDateUtc < DateTime.UtcNow &&
                x.Status != WorkTaskStatus.Done),

            EstimatedHours = estimatedHours,

            ActualHours = actualHours,

            CompletionRate = completionRate,

            ProductivityScore = productivityScore
        };

        return Result<UserProductivityResponse>
            .Success(response);
    }
}
