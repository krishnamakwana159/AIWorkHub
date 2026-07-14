using AIWorkHub.Domain.Entities;

namespace AIWorkHub.Application.Interfaces.Repositories;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
    Task<List<RefreshToken>> GetExpiredTokensAsync(
        CancellationToken cancellationToken);

    void RemoveToken(RefreshToken token);
}
