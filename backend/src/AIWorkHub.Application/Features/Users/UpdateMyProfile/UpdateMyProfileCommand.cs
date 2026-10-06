using AIWorkHub.Application.Features.Users.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Users.UpdateMyProfile;

public sealed record UpdateMyProfileCommand(UpdateProfileRequest Request)
    : IRequest<Result<UserProfileResponse>>;
