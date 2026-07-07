using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Projects.ToggleFavorite;

public sealed record ToggleFavoriteProjectCommand(Guid Id)
    : IRequest<Result>;
