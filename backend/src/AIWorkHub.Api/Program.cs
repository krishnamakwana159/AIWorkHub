using AIWorkHub.Api.Middleware;
using AIWorkHub.Api.Extensions;
using AIWorkHub.Application;
using AIWorkHub.Infrastructure;
using AIWorkHub.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddSerilogLogging();

builder.Services.AddApiServices(builder.Configuration);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPersistence(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseApiDocumentation();

app.UseSerilogRequestLogging();

app.UseAuthentication();

app.UseAuthorization();

app.MapApiEndpoints();

app.Run();

public partial class Program;
