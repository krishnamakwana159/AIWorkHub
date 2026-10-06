using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Users.ChangeMyPassword;

public sealed record ChangeMyPasswordCommand(ChangePasswordRequest Request)
    : IRequest<Result>;
