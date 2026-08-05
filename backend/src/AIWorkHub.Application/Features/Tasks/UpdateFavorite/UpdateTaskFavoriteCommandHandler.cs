using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.UpdateFavorite;

public sealed class UpdateTaskFavoriteCommandHandler(
    IWorkTaskRepository repository,
    ICurrentUserService currentUser,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateTaskFavoriteCommand, Result>
{
    public async Task<Result> Handle(
        UpdateTaskFavoriteCommand request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUser.UserId, out var ownerId))
            return Result.Failure("Unauthorized.");

        await repository.ToggleFavoriteAsync(
            request.Id,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
