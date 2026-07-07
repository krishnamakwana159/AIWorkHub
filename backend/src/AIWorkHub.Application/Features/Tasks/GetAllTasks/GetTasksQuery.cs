using AIWorkHub.Application.Features.Tasks.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.GetAllTasks;

public sealed record GetTasksQuery(Guid ProjectId)
    : IRequest<Result<IReadOnlyList<TaskResponse>>>;
