using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tags.RemoveTagFromTask;

public sealed record RemoveTagFromTaskCommand(Guid TaskId, Guid TagId)
    : IRequest<Result>;
