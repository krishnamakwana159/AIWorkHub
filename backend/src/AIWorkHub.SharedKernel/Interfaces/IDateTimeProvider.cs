namespace AIWorkHub.SharedKernel.Interfaces;

/// <summary>
/// Provides time values for application services.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Gets the current UTC date and time.
    /// </summary>
    DateTimeOffset UtcNow { get; }
}
