using AIWorkHub.Application.Features.Tasks.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.GetCalendarTasks;

public sealed record GetCalendarTasksQuery
    : IRequest<Result<IReadOnlyList<CalendarTaskResponse>>>;
