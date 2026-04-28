namespace StockAnalyst.Core.Models;

/// <summary>
/// Represents a paged result set.
/// </summary>
/// <typeparam name="T">The type of items in the result.</typeparam>
public class PagedResult<T>
{
    /// <summary>Gets the items for the current page.</summary>
    public IReadOnlyList<T> Items { get; init; } = [];

    /// <summary>Gets the current page number (1-based).</summary>
    public int Page { get; init; }

    /// <summary>Gets the number of items per page.</summary>
    public int PageSize { get; init; }

    /// <summary>Gets the total number of items across all pages.</summary>
    public int TotalCount { get; init; }

    /// <summary>Gets the total number of pages.</summary>
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
}
