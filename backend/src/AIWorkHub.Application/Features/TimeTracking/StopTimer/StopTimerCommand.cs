using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.TimeTracking.StopTimer;

public sealed record StopTimerCommand()
    : IRequest<Result>;
