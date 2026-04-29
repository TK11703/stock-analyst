using StockAnalyst.Core.Models;

namespace StockAnalyst.Core.Services;

/// <summary>
/// Provides operations for managing and querying stocks.
/// </summary>
public interface IStockService
{
    /// <summary>Gets a single stock by its ticker symbol.</summary>
    Task<Stock?> GetStockAsync(string symbol, CancellationToken ct = default);

    /// <summary>Gets multiple stocks by their ticker symbols.</summary>
    Task<IEnumerable<Stock>> GetStocksAsync(IEnumerable<string> symbols, CancellationToken ct = default);

    /// <summary>
    /// Lists stocks with server-side paging and sorting.
    /// </summary>
    /// <param name="page">1-based page number.</param>
    /// <param name="pageSize">Number of items per page (capped at 100).</param>
    /// <param name="sortBy">Column to sort by: "symbol" or "name".</param>
    /// <param name="sortDir">Sort direction: "asc" or "desc".</param>
    /// <param name="ct">Cancellation token.</param>
    Task<PagedResult<Stock>> ListStocksAsync(int page = 1, int pageSize = 20, string sortBy = "symbol", string sortDir = "asc", CancellationToken ct = default);

    /// <summary>Creates a new stock.</summary>
    /// <exception cref="InvalidOperationException">Thrown when the symbol already exists (case-insensitive).</exception>
    Task<Stock> CreateStockAsync(CreateStockRequest request, CancellationToken ct = default);
}
