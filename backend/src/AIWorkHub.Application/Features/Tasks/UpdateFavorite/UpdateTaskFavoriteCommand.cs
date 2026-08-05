using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.UpdateFavorite;

public sealed record UpdateTaskFavoriteCommand(Guid Id)
    : IRequest<Result>;
