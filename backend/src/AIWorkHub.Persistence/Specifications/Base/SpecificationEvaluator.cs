using AIWorkHub.Application.Common.Specifications;
using Microsoft.EntityFrameworkCore;

namespace AIWorkHub.Persistence.Specifications.Base;

public static class SpecificationEvaluator
{
    public static IQueryable<T> GetQuery<T>(
        IQueryable<T> inputQuery,
        ISpecification<T> specification)
        where T : class
    {
        var query = inputQuery;

        if (specification.Criteria != null)
            query = query.Where(specification.Criteria);

        query = specification.Includes.Aggregate(
            query,
            (current, include) => current.Include(include));

        if (specification.OrderBy != null)
            query = query.OrderBy(specification.OrderBy);

        if (specification.OrderByDescending != null)
            query = query.OrderByDescending(specification.OrderByDescending);

        if (specification.IsPagingEnabled)
            query = query
                .Skip(specification.Skip!.Value)
                .Take(specification.Take!.Value);

        return query;
    }
}
