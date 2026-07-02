namespace AIWorkHub.SharedKernel.Exceptions;

/// <summary>
/// Base exception for expected application errors.
/// </summary>
public abstract class AppException : Exception
{
    protected AppException(string message)
        : base(message)
    {
    }
}
