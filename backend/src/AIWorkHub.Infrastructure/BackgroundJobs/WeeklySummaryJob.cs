using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Application.Interfaces;

namespace AIWorkHub.Infrastructure.BackgroundJobs;

public sealed class WeeklySummaryJob(
    IUserRepository userRepository,
    IEmailService emailService)
{
    public async Task SendWeeklySummaryAsync(
        CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAllUsersWithTasksAsync(
            cancellationToken);

        foreach (var user in users)
        {
            var assigned = user.AssignedTasks.Count;

            var completed = user.AssignedTasks.Count(t =>
                t.Status == Domain.Enums.WorkTaskStatus.Completed);

            var pending = assigned - completed;

            await emailService.SendAsync(
                user.Email,
                "Weekly Summary",
                $"""
                <h2>Weekly Summary</h2>

                <p>Hello {user.FirstName},</p>

                <ul>
                    <li>Assigned Tasks : {assigned}</li>
                    <li>Completed Tasks : {completed}</li>
                    <li>Pending Tasks : {pending}</li>
                </ul>

                <p>Keep up the great work!</p>
                """,
                cancellationToken);
        }
    }
}
