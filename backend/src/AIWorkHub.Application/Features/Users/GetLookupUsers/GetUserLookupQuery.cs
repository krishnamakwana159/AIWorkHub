using AIWorkHub.Application.Features.Users.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Users.GetLookupUsers;

public sealed record GetUserLookupQuery()
    : IRequest<Result<IReadOnlyList<UserLookupResponse>>>;
