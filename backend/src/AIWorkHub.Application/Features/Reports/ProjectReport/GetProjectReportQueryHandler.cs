using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Features.Reports.ProjectReport;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Results;
using MediatR;

public sealed class GetProjectReportQueryHandler(
    IProjectRepository repository)
    : IRequestHandler<GetProjectReportQuery,
        Result<ProjectReportResponse>>
{
    public async Task<Result<ProjectReportResponse>> Handle(
        GetProjectReportQuery request,
        CancellationToken cancellationToken)
    {
        var project =
            await repository.GetProjectWithTasksAsync(
                request.ProjectId,
                cancellationToken);

        if (project is null)
            return Result<ProjectReportResponse>
                .Failure("Project not found.");

        var tasks = project.Tasks;

        var response = new ProjectReportResponse
        {
            ProjectId = project.Id,
            ProjectName = project.Name,

            TotalTasks = tasks.Count,

            TodoTasks = tasks.Count(x =>
                x.Status == WorkTaskStatus.Todo),

            InProgressTasks = tasks.Count(x =>
                x.Status == WorkTaskStatus.InProgress),

            ReviewTasks = tasks.Count(x =>
                x.Status == WorkTaskStatus.Review),

            DoneTasks = tasks.Count(x =>
                x.Status == WorkTaskStatus.Done),

            EstimatedHours = tasks.Sum(x =>
                x.EstimatedHours),

            ActualHours = tasks.Sum(x =>
                x.ActualHours),

            OverdueTasks = tasks.Count(x =>
                x.DueDateUtc < DateTime.UtcNow &&
                x.Status != WorkTaskStatus.Done),

            CompletionPercentage =
                tasks.Count == 0
                ? 0
                : Math.Round(
                    tasks.Count(x =>
                        x.Status == WorkTaskStatus.Done)
                    * 100m
                    / tasks.Count,
                    2)
        };

        return Result<ProjectReportResponse>
            .Success(response);
    }
}
