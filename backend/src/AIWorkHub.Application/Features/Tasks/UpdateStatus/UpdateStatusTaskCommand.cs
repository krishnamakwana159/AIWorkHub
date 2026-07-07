using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.UpdateStatus;

public sealed record UpdateTaskStatusCommand(
    Guid TaskId,
    Domain.Enums.TaskStatus Status)
    : IRequest<Result>;
