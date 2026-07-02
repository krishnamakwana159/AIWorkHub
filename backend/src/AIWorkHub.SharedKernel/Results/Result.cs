namespace AIWorkHub.SharedKernel.Results;

/// <summary>
/// Represents the outcome of an operation.
/// </summary>
public class Result
{
    protected Result(bool isSuccess, IReadOnlyCollection<string> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    /// <summary>
    /// Gets a value indicating whether the operation succeeded.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the operation failed.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Gets operation errors.
    /// </summary>
    public IReadOnlyCollection<string> Errors { get; }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    public static Result Success()
    {
        return new Result(true, Array.Empty<string>());
    }

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    public static Result Failure(params string[] errors)
    {
        return new Result(false, errors);
    }
}
