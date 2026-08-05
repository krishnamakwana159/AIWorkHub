using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.UpdatePin;

public sealed record UpdateTaskPinCommand(Guid Id)
    : IRequest<Result>;
