using AIWorkHub.Application.Features.Authentication.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Authentication.RefreshToken;

public sealed record RefreshTokenCommand(
    RefreshTokenRequest Request)
    : IRequest<Result<AuthResponse>>;
