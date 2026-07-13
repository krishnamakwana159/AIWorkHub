using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.TimeTracking.StartTimer;

public sealed record StartTimerCommand(
    StartTimerRequest Request)
    : IRequest<Result>;
