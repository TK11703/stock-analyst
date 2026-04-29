using System.Collections.Concurrent;
using StockAnalyst.Core.Models;

namespace StockAnalyst.Core.Services;

/// <summary>
/// In-memory implementation of <see cref="IStockService"/>.
/// Symbols are treated as case-insensitive for uniqueness.
/// </summary>
public class InMemoryStockService : IStockService
{
    private readonly ConcurrentDictionary<string, Stock> _stocks =
        new(StringComparer.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public Task<Stock?> GetStockAsync(string symbol, CancellationToken ct = default)
        => Task.FromResult(_stocks.TryGetValue(symbol, out var stock) ? stock : null);

    /// <inheritdoc/>
    public Task<IEnumerable<Stock>> GetStocksAsync(IEnumerable<string> symbols, CancellationToken ct = default)
    {
        var result = symbols
            .Select(s => _stocks.TryGetValue(s, out var stock) ? stock : null)
            .Where(s => s is not null)
            .Cast<Stock>();
        return Task.FromResult(result);
    }

    /// <inheritdoc/>
    public Task<PagedResult<Stock>> ListStocksAsync(
        int page = 1,
        int pageSize = 20,
        string sortBy = "symbol",
        string sortDir = "asc",
        CancellationToken ct = default)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        IEnumerable<Stock> query = _stocks.Values;

        query = sortBy.ToLowerInvariant() switch
        {
            "name" => sortDir.ToLowerInvariant() == "desc"
                ? query.OrderByDescending(s => s.Name, StringComparer.OrdinalIgnoreCase)
                : query.OrderBy(s => s.Name, StringComparer.OrdinalIgnoreCase),
            _ => sortDir.ToLowerInvariant() == "desc"
                ? query.OrderByDescending(s => s.Symbol, StringComparer.OrdinalIgnoreCase)
                : query.OrderBy(s => s.Symbol, StringComparer.OrdinalIgnoreCase),
        };

        var all = query.ToList();
        var items = all.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return Task.FromResult(new PagedResult<Stock>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = all.Count,
        });
    }

    /// <inheritdoc/>
    public Task<Stock> CreateStockAsync(CreateStockRequest request, CancellationToken ct = default)
    {
        var stock = new Stock
        {
            Symbol = request.Symbol.Trim().ToUpperInvariant(),
            Name = request.Name.Trim(),
            Price = request.Price,
            LastUpdated = DateTime.UtcNow,
        };

        if (!_stocks.TryAdd(stock.Symbol, stock))
        {
            throw new InvalidOperationException($"A stock with symbol '{stock.Symbol}' already exists.");
        }

        return Task.FromResult(stock);
    }
}
