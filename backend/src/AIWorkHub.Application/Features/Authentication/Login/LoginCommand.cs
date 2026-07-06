using AIWorkHub.Application.Features.Authentication.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Authentication.Login;

public sealed record LoginCommand(LoginRequest Request)
    : IRequest<Result<AuthResponse>>;
