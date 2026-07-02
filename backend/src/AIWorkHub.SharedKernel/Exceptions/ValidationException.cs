namespace AIWorkHub.SharedKernel.Exceptions;

/// <summary>
/// Represents a validation error.
/// </summary>
public sealed class ValidationException : AppException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class.
    /// </summary>
    public ValidationException(string message, IReadOnlyDictionary<string, string[]>? errors = null)
        : base(message)
    {
        Errors = errors ?? new Dictionary<string, string[]>();
    }

    /// <summary>
    /// Gets validation errors keyed by field name.
    /// </summary>
    public IReadOnlyDictionary<string, string[]> Errors { get; }
}
