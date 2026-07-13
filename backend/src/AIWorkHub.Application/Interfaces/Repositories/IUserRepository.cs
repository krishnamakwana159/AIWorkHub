using AIWorkHub.Domain.Entities;

namespace AIWorkHub.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    // Task AddAsync(User user, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task<User?> GetUserWithTasksAsync(
        Guid userId,
        CancellationToken cancellationToken);

    Task<List<User>> GetAllUsersWithTasksAsync(
        CancellationToken cancellationToken);
}
