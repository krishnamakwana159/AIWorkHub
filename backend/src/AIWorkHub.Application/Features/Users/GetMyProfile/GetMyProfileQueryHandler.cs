using AIWorkHub.Application.Features.Users.DTOs;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Users.GetMyProfile;

public sealed class GetMyProfileQueryHandler(
    IUserRepository repository,
    ICurrentUserService currentUser)
    : IRequestHandler<GetMyProfileQuery, Result<UserProfileResponse>>
{
    public async Task<Result<UserProfileResponse>> Handle(
        GetMyProfileQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUser.UserId, out var userId))
            return Result<UserProfileResponse>.Failure("Unauthorized.");

        var user = await repository.GetByIdAsync(userId, cancellationToken);

        if (user is null)
            return Result<UserProfileResponse>.Failure("User not found.");

        return Result<UserProfileResponse>.Success(new UserProfileResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            CreatedAtUtc = user.CreatedAtUtc
        });
    }
}
