using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Projects.Delete;

public sealed record DeleteProjectCommand(Guid Id)
    : IRequest<Result>;
