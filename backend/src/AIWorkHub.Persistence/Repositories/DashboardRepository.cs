using AIWorkHub.Application.Features.Dashboard.DTOs;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AIWorkHub.Persistence.Repositories;

public sealed class DashboardRepository(AppDbContext context)
    : IDashboardRepository
{
    public async Task<DashboardResponse> GetDashboardAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var response = new DashboardResponse
        {
            TotalProjects = await context.Projects.CountAsync(cancellationToken),

            ActiveProjects = await context.Projects
                .CountAsync(
                    x => x.Status == ProjectStatus.Active,
                    cancellationToken),

            TotalTasks = await context.WorkTasks
                .CountAsync(cancellationToken),

            TodoTasks = await context.WorkTasks
                .CountAsync(
                    x => x.Status == WorkTaskStatus.Todo,
                    cancellationToken),

            InProgressTasks = await context.WorkTasks
                .CountAsync(
                    x => x.Status == WorkTaskStatus.InProgress,
                    cancellationToken),

            CompletedTasks = await context.WorkTasks
                .CountAsync(
                    x => x.Status == WorkTaskStatus.Completed,
                    cancellationToken),

            OverdueTasks = await context.WorkTasks
                .CountAsync(
                    x => x.DueDateUtc != null &&
                         x.DueDateUtc < DateTime.UtcNow &&
                         x.Status != WorkTaskStatus.Completed,
                    cancellationToken),

            MyPendingTasks = await context.WorkTasks
                .CountAsync(
                    x => x.AssignedUserId == userId &&
                         x.Status != WorkTaskStatus.Completed,
                    cancellationToken),

            UnreadNotifications = await context.Notifications
                .CountAsync(
                    x => x.UserId == userId &&
                         !x.IsRead,
                    cancellationToken)
        };

        response.RecentProjects = await context.Projects
            .OrderByDescending(x => x.CreatedAtUtc)
            .Take(5)
            .Select(x => new ProjectDashboardDto
            {
                Id = x.Id,
                Name = x.Name,
                Status = x.Status.ToString(),
                CreatedAtUtc = x.CreatedAtUtc
            })
            .ToListAsync(cancellationToken);

        response.RecentTasks = await context.WorkTasks
            .Include(x => x.Project)
            .OrderByDescending(x => x.CreatedAtUtc)
            .Take(5)
            .Select(x => new TaskDashboardDto
            {
                Id = x.Id,
                Title = x.Title,
                Status = x.Status.ToString(),
                ProjectId = x.ProjectId,
                ProjectName = x.Project.Name
            })
            .ToListAsync(cancellationToken);

        response.RecentActivities = await context.ActivityLogs
            .OrderByDescending(x => x.CreatedAtUtc)
            .Take(10)
            .Select(x => new ActivityDashboardDto
            {
                Id = x.Id,
                Action = x.Action.ToString(),
                Description = x.Description,
                CreatedAtUtc = x.CreatedAtUtc
            })
            .ToListAsync(cancellationToken);

        return response;
    }
}
