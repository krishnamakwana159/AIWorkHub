using AIWorkHub.Application.Features.Users.DTOs;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Users.GetLookupUsers;

public sealed class GetUserLookupQueryHandler(
    IUserRepository repository)
    : IRequestHandler<GetUserLookupQuery,
        Result<IReadOnlyList<UserLookupResponse>>>
{
    public async Task<Result<IReadOnlyList<UserLookupResponse>>> Handle(
        GetUserLookupQuery request,
        CancellationToken cancellationToken)
    {
        var users =
            await repository.GetAllUsersWithTasksAsync(
                cancellationToken);

        var response = users
            .Select(x => new UserLookupResponse
            {
                Id = x.Id,
                FullName =
                    $"{x.FirstName} {x.LastName}",
                Email = x.Email
            })
            .OrderBy(x => x.FullName)
            .ToList();

        return Result<IReadOnlyList<UserLookupResponse>>
            .Success(response);
    }
}
