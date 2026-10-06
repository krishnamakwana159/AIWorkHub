using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tags.AddTagToTask;

public sealed record AddTagToTaskCommand(Guid TaskId, Guid TagId)
    : IRequest<Result>;
