using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using StockAnalyst.Core.Models;

namespace StockAnalyst.Api.Tests;

public class StockApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public StockApiTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    private HttpClient CreateClient() => _factory.CreateClient();

    [Fact]
    public async Task GetStocks_ReturnsOkWithPagedResult()
    {
        var client = CreateClient();
        var response = await client.GetAsync("/api/stocks");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PagedResult<StockDto>>();
        Assert.NotNull(result);
        Assert.Equal(1, result.Page);
        Assert.Equal(20, result.PageSize);
        Assert.Equal(0, result.TotalCount);
        Assert.Empty(result.Items);
    }

    [Fact]
    public async Task PostStock_CreatesStockAndAppearsInGet()
    {
        var client = CreateClient();
        var request = new { Symbol = "TEST1", Name = "Test Stock One", Price = 100m };
        var postResponse = await client.PostAsJsonAsync("/api/stocks", request);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var getResponse = await client.GetAsync("/api/stocks");
        getResponse.EnsureSuccessStatusCode();
        var result = await getResponse.Content.ReadFromJsonAsync<PagedResult<StockDto>>();
        Assert.NotNull(result);
        Assert.Contains(result.Items, s => s.Symbol == "TEST1");
    }

    [Fact]
    public async Task PostStock_DuplicateSymbol_ReturnsConflict()
    {
        var client = CreateClient();
        var request = new { Symbol = "DUPL1", Name = "Duplicate Stock", Price = 0m };
        var first = await client.PostAsJsonAsync("/api/stocks", request);
        Assert.Equal(HttpStatusCode.Created, first.StatusCode);

        var second = await client.PostAsJsonAsync("/api/stocks", request);
        Assert.Equal(HttpStatusCode.Conflict, second.StatusCode);
    }

    [Fact]
    public async Task PostStock_MissingSymbol_ReturnsBadRequest()
    {
        var client = CreateClient();
        var request = new { Symbol = "", Name = "No Symbol Stock", Price = 0m };
        var response = await client.PostAsJsonAsync("/api/stocks", request);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostStock_MissingName_ReturnsBadRequest()
    {
        var client = CreateClient();
        var request = new { Symbol = "NOSYM", Name = "", Price = 0m };
        var response = await client.PostAsJsonAsync("/api/stocks", request);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetStocks_SortingAndPagingParametersAreRespected()
    {
        var client = CreateClient();
        var response = await client.GetAsync("/api/stocks?page=1&pageSize=5&sortBy=name&sortDir=desc");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PagedResult<StockDto>>();
        Assert.NotNull(result);
        Assert.Equal(1, result.Page);
        Assert.Equal(5, result.PageSize);
    }

    private record StockDto(string Symbol, string Name, decimal Price, DateTime LastUpdated);
}

