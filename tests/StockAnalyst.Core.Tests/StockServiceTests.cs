using StockAnalyst.Core.Models;
using StockAnalyst.Core.Services;

namespace StockAnalyst.Core.Tests;

public class StockServiceTests
{
    private static InMemoryStockService CreateServiceWithStocks(params (string Symbol, string Name)[] stocks)
    {
        var service = new InMemoryStockService();
        foreach (var (symbol, name) in stocks)
        {
            service.CreateStockAsync(new CreateStockRequest { Symbol = symbol, Name = name }).GetAwaiter().GetResult();
        }
        return service;
    }

    [Fact]
    public async Task ListStocksAsync_EmptyStore_ReturnsEmptyResult()
    {
        var service = new InMemoryStockService();
        var result = await service.ListStocksAsync();
        Assert.Equal(0, result.TotalCount);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalPages);
    }

    [Fact]
    public async Task ListStocksAsync_ReturnsTotalCount()
    {
        var service = CreateServiceWithStocks(("AAPL", "Apple"), ("GOOG", "Google"), ("MSFT", "Microsoft"));
        var result = await service.ListStocksAsync(page: 1, pageSize: 10);
        Assert.Equal(3, result.TotalCount);
    }

    [Fact]
    public async Task ListStocksAsync_Paging_ReturnsCorrectPage()
    {
        var service = CreateServiceWithStocks(
            ("A", "Alpha"), ("B", "Beta"), ("C", "Gamma"), ("D", "Delta"), ("E", "Epsilon"));

        var page1 = await service.ListStocksAsync(page: 1, pageSize: 2, sortBy: "symbol", sortDir: "asc");
        var page2 = await service.ListStocksAsync(page: 2, pageSize: 2, sortBy: "symbol", sortDir: "asc");
        var page3 = await service.ListStocksAsync(page: 3, pageSize: 2, sortBy: "symbol", sortDir: "asc");

        Assert.Equal(2, page1.Items.Count);
        Assert.Equal("A", page1.Items[0].Symbol);
        Assert.Equal("B", page1.Items[1].Symbol);

        Assert.Equal(2, page2.Items.Count);
        Assert.Equal("C", page2.Items[0].Symbol);
        Assert.Equal("D", page2.Items[1].Symbol);

        Assert.Single(page3.Items);
        Assert.Equal("E", page3.Items[0].Symbol);

        Assert.Equal(5, page1.TotalCount);
        Assert.Equal(3, page1.TotalPages);
    }

    [Fact]
    public async Task ListStocksAsync_SortBySymbolAsc_ReturnsCorrectOrder()
    {
        var service = CreateServiceWithStocks(("ZZZ", "Zzz"), ("AAA", "Aaa"), ("MMM", "Mmm"));
        var result = await service.ListStocksAsync(sortBy: "symbol", sortDir: "asc");
        var symbols = result.Items.Select(s => s.Symbol).ToList();
        Assert.Equal(["AAA", "MMM", "ZZZ"], symbols);
    }

    [Fact]
    public async Task ListStocksAsync_SortBySymbolDesc_ReturnsCorrectOrder()
    {
        var service = CreateServiceWithStocks(("ZZZ", "Zzz"), ("AAA", "Aaa"), ("MMM", "Mmm"));
        var result = await service.ListStocksAsync(sortBy: "symbol", sortDir: "desc");
        var symbols = result.Items.Select(s => s.Symbol).ToList();
        Assert.Equal(["ZZZ", "MMM", "AAA"], symbols);
    }

    [Fact]
    public async Task ListStocksAsync_SortByNameAsc_ReturnsCorrectOrder()
    {
        var service = CreateServiceWithStocks(("C", "Zebra"), ("A", "Apple"), ("B", "Mango"));
        var result = await service.ListStocksAsync(sortBy: "name", sortDir: "asc");
        var names = result.Items.Select(s => s.Name).ToList();
        Assert.Equal(["Apple", "Mango", "Zebra"], names);
    }

    [Fact]
    public async Task ListStocksAsync_SortByNameDesc_ReturnsCorrectOrder()
    {
        var service = CreateServiceWithStocks(("C", "Zebra"), ("A", "Apple"), ("B", "Mango"));
        var result = await service.ListStocksAsync(sortBy: "name", sortDir: "desc");
        var names = result.Items.Select(s => s.Name).ToList();
        Assert.Equal(["Zebra", "Mango", "Apple"], names);
    }

    [Fact]
    public async Task CreateStockAsync_ValidRequest_ReturnsCreatedStock()
    {
        var service = new InMemoryStockService();
        var request = new CreateStockRequest { Symbol = "TSLA", Name = "Tesla Inc.", Price = 200m };
        var stock = await service.CreateStockAsync(request);

        Assert.Equal("TSLA", stock.Symbol);
        Assert.Equal("Tesla Inc.", stock.Name);
        Assert.Equal(200m, stock.Price);
    }

    [Fact]
    public async Task CreateStockAsync_NormalizesSymbolToUppercase()
    {
        var service = new InMemoryStockService();
        var stock = await service.CreateStockAsync(new CreateStockRequest { Symbol = "tsla", Name = "Tesla" });
        Assert.Equal("TSLA", stock.Symbol);
    }

    [Fact]
    public async Task CreateStockAsync_DuplicateSymbol_ThrowsInvalidOperationException()
    {
        var service = new InMemoryStockService();
        await service.CreateStockAsync(new CreateStockRequest { Symbol = "ABC", Name = "Alpha Beta Corp" });

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => service.CreateStockAsync(new CreateStockRequest { Symbol = "ABC", Name = "Another Corp" }));
    }

    [Fact]
    public async Task CreateStockAsync_DuplicateSymbolCaseInsensitive_ThrowsInvalidOperationException()
    {
        var service = new InMemoryStockService();
        await service.CreateStockAsync(new CreateStockRequest { Symbol = "ABC", Name = "Alpha Beta Corp" });

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => service.CreateStockAsync(new CreateStockRequest { Symbol = "abc", Name = "Another Corp" }));
    }

    [Fact]
    public async Task CreateStockAsync_StockAppearsInList()
    {
        var service = new InMemoryStockService();
        await service.CreateStockAsync(new CreateStockRequest { Symbol = "NVDA", Name = "Nvidia" });
        var result = await service.ListStocksAsync();
        Assert.Single(result.Items);
        Assert.Equal("NVDA", result.Items[0].Symbol);
    }
}

