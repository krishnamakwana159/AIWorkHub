namespace AIWorkHub.SharedKernel.Exceptions;

/// <summary>
/// Represents a missing resource error.
/// </summary>
public sealed class NotFoundException : AppException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class.
    /// </summary>
    public NotFoundException(string message)
        : base(message)
    {
    }
}
