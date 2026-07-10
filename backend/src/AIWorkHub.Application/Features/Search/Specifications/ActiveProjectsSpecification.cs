using AIWorkHub.Application.Common.Specifications;
using AIWorkHub.Domain.Entities;
using AIWorkHub.Domain.Enums;

namespace AIWorkHub.Application.Features.Search.Specifications;

public sealed class ActiveProjectsSpecification
    : Specification<Project>
{
    public ActiveProjectsSpecification()
    {
        AddCriteria(x =>
            !x.IsArchived &&
            x.Status != ProjectStatus.Completed);

        ApplyOrderByDescending(x => x.CreatedAtUtc);
    }
}
