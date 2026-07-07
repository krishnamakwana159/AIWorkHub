using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.DeleteTask;

public sealed record DeleteTaskCommand(Guid Id)
    : IRequest<Result>;
