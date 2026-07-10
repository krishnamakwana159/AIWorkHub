using AIWorkHub.Application.Common.Specifications;
using AIWorkHub.Domain.Entities;

namespace AIWorkHub.Application.Features.Search.Specifications;

public sealed class CommentSearchSpecification
    : Specification<TaskComment>
{
    public CommentSearchSpecification(string query)
    {
        AddCriteria(x => x.Comment.Contains(query));

        ApplyOrderByDescending(x => x.CreatedAtUtc);
    }
}
