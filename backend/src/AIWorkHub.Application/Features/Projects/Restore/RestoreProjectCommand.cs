using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Projects.Restore;

public sealed record RestoreProjectCommand(Guid Id)
    : IRequest<Result>;
