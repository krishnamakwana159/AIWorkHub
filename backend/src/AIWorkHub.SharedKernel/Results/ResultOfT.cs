namespace AIWorkHub.SharedKernel.Results;

/// <summary>
/// Represents the outcome of an operation that returns a value.
/// </summary>
/// <typeparam name="T">The result value type.</typeparam>
public sealed class Result<T> : Result
{
    private Result(T? value, bool isSuccess, IReadOnlyCollection<string> errors)
        : base(isSuccess, errors)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the operation value when successful.
    /// </summary>
    public T? Value { get; }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    public static Result<T> Success(T value)
    {
        return new Result<T>(value, true, Array.Empty<string>());
    }

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    public new static Result<T> Failure(params string[] errors)
    {
        return new Result<T>(default, false, errors);
    }
}
