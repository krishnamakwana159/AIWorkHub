using System.Linq.Expressions;
using AIWorkHub.Application.Common.Specifications;

namespace AIWorkHub.Application.Common.Specifications;

public abstract class Specification<T> : ISpecification<T>
{
    public Expression<Func<T, bool>>? Criteria { get; private set; }

    public List<Expression<Func<T, object>>> Includes { get; } = new();

    public Expression<Func<T, object>>? OrderBy { get; private set; }

    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    public int? Skip { get; private set; }

    public int? Take { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    protected void AddCriteria(Expression<Func<T, bool>> criteria)
        => Criteria = criteria;

    protected void AddInclude(Expression<Func<T, object>> include)
        => Includes.Add(include);

    protected void ApplyOrderBy(Expression<Func<T, object>> expression)
        => OrderBy = expression;

    protected void ApplyOrderByDescending(Expression<Func<T, object>> expression)
        => OrderByDescending = expression;

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}
