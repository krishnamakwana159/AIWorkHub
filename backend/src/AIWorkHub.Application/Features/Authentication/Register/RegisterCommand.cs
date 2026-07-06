using AIWorkHub.Application.Features.Authentication.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Authentication.Register;

public sealed record RegisterCommand(RegisterRequest Request)
    : IRequest<Result<AuthResponse>>;
