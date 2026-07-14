using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIWorkHub.Persistence.Repositories;

public sealed class RefreshTokenRepository(AppDbContext context)
    : RepositoryBase<RefreshToken>(context), IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetByTokenAsync(
        string token,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(x => x.User)
            .FirstOrDefaultAsync(
                x => x.Token == token,
                cancellationToken);
    }

    public async Task<List<RefreshToken>> GetExpiredTokensAsync(
        CancellationToken cancellationToken = default)
    {
        return await context.RefreshTokens
            .Where(x =>
                x.IsRevoked ||
                x.ExpiresAtUtc < DateTime.UtcNow.AddDays(-7))
            .ToListAsync(cancellationToken);
    }

    public void RemoveToken(RefreshToken token)
    {
        context.RefreshTokens.Remove(token);
    }
}
