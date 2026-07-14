using AIWorkHub.Api.Middleware;
using AIWorkHub.Api.Extensions;
using AIWorkHub.Application;
using AIWorkHub.Infrastructure;
using AIWorkHub.Persistence;
using Serilog;
using AIWorkHub.Infrastructure.Hubs;
using Hangfire;
using AIWorkHub.Infrastructure.BackgroundJobs;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddSerilogLogging();

builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddSignalR();
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPersistence(builder.Configuration);

builder.Services.AddHangfire(configuration =>
{
    configuration.UseSqlServerStorage(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHangfireServer();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseApiDocumentation();

app.UseSerilogRequestLogging();

app.UseAuthentication();

app.UseAuthorization();

app.UseHangfireDashboard("/hangfire");

RecurringJob.AddOrUpdate<ReminderJob>(
    "daily-reminders",
    job => job.SendDailyRemindersAsync(CancellationToken.None),
    Cron.Daily);

RecurringJob.AddOrUpdate<WeeklySummaryJob>(
    "weekly-summary",
    job => job.SendWeeklySummaryAsync(CancellationToken.None),
    Cron.Weekly);

RecurringJob.AddOrUpdate<CleanupJob>(
    "cleanup-refresh-tokens",
    job => job.CleanupAsync(CancellationToken.None),
    Cron.Daily);

app.MapApiEndpoints();

app.MapHub<NotificationHub>("/hubs/notifications");

app.MapHub<KanbanHub>("/hubs/kanban");

app.Run();

public partial class Program;
