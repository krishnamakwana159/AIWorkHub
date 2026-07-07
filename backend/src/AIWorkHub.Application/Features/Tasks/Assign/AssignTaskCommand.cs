using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.Assign;

public sealed record AssignTaskCommand(
    Guid TaskId,
    Guid UserId)
    : IRequest<Result>;
