using AIWorkHub.SharedKernel.Interfaces;

namespace AIWorkHub.Infrastructure.Services;

/// <summary>
/// System clock implementation for UTC timestamps.
/// </summary>
public sealed class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc />
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
