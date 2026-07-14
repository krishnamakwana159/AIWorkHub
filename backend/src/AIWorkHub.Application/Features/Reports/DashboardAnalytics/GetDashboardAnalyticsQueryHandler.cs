using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Reports.DashboardAnalytics;

public sealed class GetDashboardAnalyticsQueryHandler(
    IProjectRepository projectRepository,
    IWorkTaskRepository taskRepository,
    IUserRepository userRepository,
    IWorkTimeEntryRepository timeRepository)
    : IRequestHandler<GetDashboardAnalyticsQuery, Result<DashboardAnalyticsResponse>>
{
    public async Task<Result<DashboardAnalyticsResponse>> Handle(
        GetDashboardAnalyticsQuery request,
        CancellationToken cancellationToken)
    {
        var projects = await projectRepository.GetAllProjectsAsync(
            cancellationToken);

        var tasks = await taskRepository.GetAllTasksAsync(
            cancellationToken);

        var users = await userRepository.GetAllUsersWithTasksAsync(
            cancellationToken);

        var timeEntries = await timeRepository.GetAllAsync(
            cancellationToken);

        var response = new DashboardAnalyticsResponse
        {
            Overview = BuildOverview(projects, tasks, users, timeEntries),

            MonthlyTrend = BuildMonthlyTrend(tasks, timeEntries),

            ProjectProgress = BuildProjectProgress(projects),

            TeamPerformance = BuildTeamPerformance(users),

            TopPerformers = BuildTopPerformers(users)
        };

        return Result<DashboardAnalyticsResponse>.Success(response);
    }

    private static KPIOverviewDto BuildOverview(
        List<Project> projects,
        List<WorkTask> tasks,
        List<User> users,
        List<WorkTimeEntry> entries)
    {
        var completedTasks = tasks.Count(x => x.Status == WorkTaskStatus.Completed);

        var overdueTasks = tasks.Count(x =>
            x.DueDateUtc.HasValue &&
            x.DueDateUtc.Value < DateTime.UtcNow &&
            x.Status != WorkTaskStatus.Completed);

        var estimated = tasks.Sum(x => x.EstimatedHours);

        var actual = entries.Sum(x => x.Hours);

        return new KPIOverviewDto
        {
            TotalProjects = projects.Count,

            TotalUsers = users.Count,

            TotalTasks = tasks.Count,

            CompletedTasks = completedTasks,

            ActiveTasks = tasks.Count - completedTasks,

            OverdueTasks = overdueTasks,

            EstimatedHours = estimated,

            ActualHours = actual,

            CompletionPercentage =
                tasks.Count == 0
                    ? 0
                    : Math.Round(
                        (decimal)completedTasks * 100 / tasks.Count,
                        2)
        };
    }

    private static List<MonthlyTrendDto> BuildMonthlyTrend(
        List<WorkTask> tasks,
        List<WorkTimeEntry> entries)
    {
        var result = tasks
            .GroupBy(x => new
            {
                x.CreatedAtUtc.Year,
                x.CreatedAtUtc.Month
            })
            .Select(group => new MonthlyTrendDto
            {
                Year = group.Key.Year,

                Month = group.Key.Month,

                TasksCreated = group.Count(),

                TasksCompleted =
                    group.Count(x => x.Status == WorkTaskStatus.Completed),

                HoursLogged = entries
                    .Where(e =>
                        e.StartTimeUtc.Year == group.Key.Year &&
                        e.StartTimeUtc.Month == group.Key.Month)
                    .Sum(e => e.Hours)
            })
            .OrderBy(x => x.Year)
            .ThenBy(x => x.Month)
            .ToList();

        return result;
    }

        private static List<ProjectProgressDto> BuildProjectProgress(
        List<Project> projects)
    {
        return projects
            .Select(project =>
            {
                var total = project.Tasks.Count;

                var completed = project.Tasks.Count(x =>
                    x.Status == WorkTaskStatus.Completed);

                return new ProjectProgressDto
                {
                    ProjectId = project.Id,

                    ProjectName = project.Name,

                    Progress = total == 0
                        ? 0
                        : Math.Round((decimal)completed * 100 / total, 2),

                    TotalTasks = total,

                    CompletedTasks = completed,

                    EstimatedHours =
                        project.Tasks.Sum(x => x.EstimatedHours),

                    ActualHours =
                        project.Tasks.Sum(x => x.ActualHours)
                };
            })
            .OrderByDescending(x => x.Progress)
            .ToList();
    }

    private static List<TeamPerformanceDto> BuildTeamPerformance(
        List<User> users)
    {
        return users
            .Select(user =>
            {
                var tasks = user.AssignedTasks.ToList();

                var completed = tasks.Count(x =>
                    x.Status == WorkTaskStatus.Completed);

                return new TeamPerformanceDto
                {
                    UserId = user.Id,

                    UserName =
                        $"{user.FirstName} {user.LastName}".Trim(),

                    AssignedTasks = tasks.Count,

                    CompletedTasks = completed,

                    CompletionRate =
                        tasks.Count == 0
                            ? 0
                            : Math.Round(
                                (decimal)completed * 100 / tasks.Count,
                                2),

                    HoursLogged =
                        tasks.Sum(x => x.ActualHours)
                };
            })
            .OrderByDescending(x => x.CompletionRate)
            .ToList();
    }

    private static List<TopPerformerDto> BuildTopPerformers(
        List<User> users)
    {
        return users
            .Select(user =>
            {
                var tasks = user.AssignedTasks.ToList();

                var completed =
                    tasks.Count(x => x.Status == WorkTaskStatus.Completed);

                var score =
                    completed * 10
                    - tasks.Count(x =>
                        x.DueDateUtc < DateTime.UtcNow &&
                        x.Status != WorkTaskStatus.Completed) * 2
                    + (int)tasks.Sum(x => x.ActualHours);

                return new TopPerformerDto
                {
                    UserId = user.Id,

                    UserName =
                        $"{user.FirstName} {user.LastName}".Trim(),

                    ProductivityScore = score
                };
            })
            .OrderByDescending(x => x.ProductivityScore)
            .Take(5)
            .ToList();
}
}
