using AIWorkHub.Application.Features.Tasks.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.GetTaskById;

public sealed record GetTaskByIdQuery(Guid Id)
    : IRequest<Result<TaskResponse>>;
