using AIWorkHub.Application.Features.Users.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Users.GetMyProfile;

public sealed record GetMyProfileQuery
    : IRequest<Result<UserProfileResponse>>;
