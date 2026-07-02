namespace AIWorkHub.SharedKernel.Exceptions;

/// <summary>
/// Represents a resource conflict error.
/// </summary>
public sealed class ConflictException : AppException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConflictException"/> class.
    /// </summary>
    public ConflictException(string message)
        : base(message)
    {
    }
}
