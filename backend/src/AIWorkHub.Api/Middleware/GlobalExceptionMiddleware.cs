using System.Diagnostics;
using AIWorkHub.SharedKernel.Exceptions;
using Microsoft.AspNetCore.Mvc;
using UnauthorizedException = AIWorkHub.SharedKernel.Exceptions.UnauthorizedException;
using ValidationException = AIWorkHub.SharedKernel.Exceptions.ValidationException;

namespace AIWorkHub.Api.Middleware;

/// <summary>
/// Converts unhandled exceptions into RFC 7807 problem details responses.
/// </summary>
public sealed class GlobalExceptionMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionMiddleware> logger,
    IHostEnvironment environment)
{
    /// <summary>
    /// Invokes the middleware.
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var problemDetails = CreateProblemDetails(context, exception);

        if (exception is AppException)
        {
            logger.LogWarning(exception, "Handled application exception for {Method} {Path}.",
                context.Request.Method,
                context.Request.Path);
        }
        else
        {
            logger.LogError(exception, "Unhandled exception occurred while processing {Method} {Path}.",
                context.Request.Method,
                context.Request.Path);
        }

        context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problemDetails);
    }

    private ProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
    {
        var (statusCode, title) = exception switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, "Resource not found"),
            ValidationException => (StatusCodes.Status400BadRequest, "Validation failed"),
            ConflictException => (StatusCodes.Status409Conflict, "Conflict"),
            UnauthorizedException => (StatusCodes.Status401Unauthorized, "Unauthorized"),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = environment.IsDevelopment() ? exception.Message : GetSafeDetail(statusCode),
            Instance = context.Request.Path
        };

        problemDetails.Extensions["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier;

        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions["errors"] = validationException.Errors;
        }

        return problemDetails;
    }

    private static string GetSafeDetail(int statusCode)
    {
        return statusCode == StatusCodes.Status500InternalServerError
            ? "The request could not be completed."
            : "The request could not be processed.";
    }
}
