using AIWorkHub.Application.Common.Specifications;
using AIWorkHub.Domain.Entities;

namespace AIWorkHub.Application.Features.Search.Specifications;

public sealed class UserSearchSpecification
    : Specification<User>
{
    public UserSearchSpecification(string query)
    {
        AddCriteria(x =>
            x.FirstName.Contains(query) ||
            x.LastName.Contains(query) ||
            x.Email.Contains(query));

        ApplyOrderBy(x => x.FirstName);
    }
}
