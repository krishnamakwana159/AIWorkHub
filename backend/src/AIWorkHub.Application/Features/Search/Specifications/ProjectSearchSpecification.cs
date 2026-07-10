using AIWorkHub.Application.Common.Specifications;
using AIWorkHub.Domain.Entities;

namespace AIWorkHub.Application.Features.Search.Specifications;

public sealed class ProjectSearchSpecification
    : Specification<Project>
{
    public ProjectSearchSpecification(string query)
    {
        AddCriteria(x =>
            !x.IsArchived &&
            (
                x.Name.Contains(query) ||
                (x.Description != null &&
                 x.Description.Contains(query))
            ));

        ApplyOrderBy(x => x.Name);
    }
}
