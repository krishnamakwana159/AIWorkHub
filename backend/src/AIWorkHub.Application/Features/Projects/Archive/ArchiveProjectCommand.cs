using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Projects.Archive;

public sealed record ArchiveProjectCommand(Guid Id)
    : IRequest<Result>;
