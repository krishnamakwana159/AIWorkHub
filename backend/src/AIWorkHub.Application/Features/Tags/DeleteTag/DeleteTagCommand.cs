using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tags.DeleteTag;

public sealed record DeleteTagCommand(Guid TagId)
    : IRequest<Result>;
