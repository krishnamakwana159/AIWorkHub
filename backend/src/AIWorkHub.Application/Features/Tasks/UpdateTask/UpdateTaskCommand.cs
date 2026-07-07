using AIWorkHub.Application.Features.Tasks.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.UpdateTask;

public sealed record UpdateTaskCommand(
    Guid Id,
    UpdateTaskRequest Request)
    : IRequest<Result<TaskResponse>>;
