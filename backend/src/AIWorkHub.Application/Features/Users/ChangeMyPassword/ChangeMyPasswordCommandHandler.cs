using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Users.ChangeMyPassword;

public sealed class ChangeMyPasswordCommandHandler(
    IUserRepository repository,
    ICurrentUserService currentUser,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ChangeMyPasswordCommand, Result>
{
    public async Task<Result> Handle(
        ChangeMyPasswordCommand request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUser.UserId, out var userId))
            return Result.Failure("Unauthorized.");

        var user = await repository.GetByIdAsync(userId, cancellationToken);

        if (user is null)
            return Result.Failure("User not found.");

        if (!passwordHasher.VerifyPassword(
                request.Request.CurrentPassword,
                user.PasswordHash))
        {
            return Result.Failure("Current password is incorrect.");
        }

        user.PasswordHash = passwordHasher.HashPassword(
            request.Request.NewPassword);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
