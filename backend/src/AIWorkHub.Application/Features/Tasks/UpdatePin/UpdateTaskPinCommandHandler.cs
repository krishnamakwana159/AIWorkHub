using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.UpdatePin;

public sealed class UpdateTaskPinCommandHandler(
    IWorkTaskRepository repository,
    ICurrentUserService currentUser,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateTaskPinCommand, Result>
{
    public async Task<Result> Handle(
        UpdateTaskPinCommand request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUser.UserId, out var ownerId))
            return Result.Failure("Unauthorized.");

        await repository.TogglePinAsync(
            request.Id,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
