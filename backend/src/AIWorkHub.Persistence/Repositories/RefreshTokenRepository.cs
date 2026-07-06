using AIWorkHub.Application.Interfaces;
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
}
