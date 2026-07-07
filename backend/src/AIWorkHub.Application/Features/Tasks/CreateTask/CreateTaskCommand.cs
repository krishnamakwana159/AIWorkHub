using AIWorkHub.Application.Features.Tasks.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.CreateTask;

public sealed record CreateTaskCommand(CreateTaskRequest Request)
    : IRequest<Result<TaskResponse>>;
