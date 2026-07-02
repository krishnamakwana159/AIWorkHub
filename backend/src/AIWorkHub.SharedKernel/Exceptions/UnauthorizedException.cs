namespace AIWorkHub.SharedKernel.Exceptions;

/// <summary>
/// Represents an authorization error.
/// </summary>
public sealed class UnauthorizedException : AppException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnauthorizedException"/> class.
    /// </summary>
    public UnauthorizedException(string message)
        : base(message)
    {
    }
}
