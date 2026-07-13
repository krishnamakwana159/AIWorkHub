using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Kanban.MoveTask;

public sealed record MoveTaskCommand(
    Guid TaskId,
    MoveTaskRequest Request)
    : IRequest<Result>;
