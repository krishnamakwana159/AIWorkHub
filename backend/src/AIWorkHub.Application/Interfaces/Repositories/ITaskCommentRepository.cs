using AIWorkHub.Domain.Entities;

namespace AIWorkHub.Application.Interfaces.Repositories;

public interface ITaskCommentRepository : IRepository<TaskComment>
{
    Task<IReadOnlyList<TaskComment>> GetByTaskIdAsync(
        Guid taskId,
        CancellationToken cancellationToken);

    Task<bool> CommentExistsAsync(
        Guid commentId,
        CancellationToken cancellationToken);

    Task<TaskComment?> GetByIdWithUserAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
