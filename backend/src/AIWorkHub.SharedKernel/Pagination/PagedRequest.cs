namespace AIWorkHub.SharedKernel.Pagination;

/// <summary>
/// Represents a paged query request.
/// </summary>
public sealed record PagedRequest
{
    private const int MaxPageSize = 100;
    private int _pageNumber = 1;
    private int _pageSize = 20;

    /// <summary>
    /// Gets or initializes the one-based page number.
    /// </summary>
    public int PageNumber
    {
        get => _pageNumber;
        init => _pageNumber = value < 1 ? 1 : value;
    }

    /// <summary>
    /// Gets or initializes the page size.
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = value switch
        {
            < 1 => 20,
            > MaxPageSize => MaxPageSize,
            _ => value
        };
    }
}
