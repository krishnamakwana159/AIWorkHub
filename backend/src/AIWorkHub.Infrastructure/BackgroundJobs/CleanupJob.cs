using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Interfaces;

namespace AIWorkHub.Infrastructure.BackgroundJobs;

public sealed class CleanupJob(
    IRefreshTokenRepository refreshTokenRepository,
    IUnitOfWork unitOfWork)
{
    public async Task CleanupAsync(
        CancellationToken cancellationToken)
    {
        var tokens = await refreshTokenRepository
            .GetExpiredTokensAsync(cancellationToken);

        if (tokens.Count == 0)
            return;

        foreach (var token in tokens)
        {
            refreshTokenRepository.RemoveToken(token);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
