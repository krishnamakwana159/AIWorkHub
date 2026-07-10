using AIWorkHub.Application.Common.Specifications;
using AIWorkHub.Domain.Entities;

namespace AIWorkHub.Application.Features.Search.Specifications;

public sealed class TaskSearchSpecification
    : Specification<WorkTask>
{
    public TaskSearchSpecification(string query)
    {
        AddCriteria(x =>
            x.Title.Contains(query) ||
            (x.Description != null &&
             x.Description.Contains(query)));

        AddInclude(x => x.Project);

        ApplyOrderBy(x => x.Title);
    }
}
