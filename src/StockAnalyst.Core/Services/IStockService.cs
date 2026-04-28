using StockAnalyst.Core.Models;

namespace StockAnalyst.Core.Services;

public interface IStockService
{
    Task<Stock?> GetStockAsync(string symbol);
    Task<IEnumerable<Stock>> GetStocksAsync(IEnumerable<string> symbols);
}
