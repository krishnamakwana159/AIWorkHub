using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Enums;

namespace AIWorkHub.Infrastructure.BackgroundJobs;

public sealed class ReminderJob(
    IWorkTaskRepository taskRepository,
    IEmailService emailService,
    INotificationService notificationService)
{
    public async Task SendDailyRemindersAsync(
        CancellationToken cancellationToken)
    {
        var tasks = await taskRepository.GetAllTasksAsync(cancellationToken);

        var overdueTasks = tasks
            .Where(t =>
                t.AssignedUser != null &&
                t.DueDateUtc.HasValue &&
                t.DueDateUtc.Value.Date <= DateTime.UtcNow.Date &&
                t.Status != WorkTaskStatus.Completed)
            .ToList();

        foreach (var task in overdueTasks)
        {
            await emailService.SendAsync(
                task.AssignedUser!.Email,
                "Task Reminder",
                $"""
                <h2>Task Reminder</h2>

                <p>Your task <strong>{task.Title}</strong> is overdue.</p>

                <p>Please complete it as soon as possible.</p>
                """,
                cancellationToken);

            await notificationService.NotifyAsync(
                task.AssignedUser.Id,
                "Task Reminder",
                $"Task '{task.Title}' is overdue.",
                NotificationType.Warning,
                $"/tasks/{task.Id}",
                cancellationToken);
        }
    }
}
