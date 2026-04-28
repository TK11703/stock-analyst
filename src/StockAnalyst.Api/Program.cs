using StockAnalyst.Core.Models;
using StockAnalyst.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IStockService, InMemoryStockService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/api/stocks", async (
    IStockService stockService,
    int page = 1,
    int pageSize = 20,
    string sortBy = "symbol",
    string sortDir = "asc") =>
{
    var result = await stockService.ListStocksAsync(page, pageSize, sortBy, sortDir);
    return Results.Ok(result);
})
.WithName("ListStocks");

app.MapPost("/api/stocks", async (CreateStockRequest request, IStockService stockService) =>
{
    var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
    var context = new System.ComponentModel.DataAnnotations.ValidationContext(request);
    if (!System.ComponentModel.DataAnnotations.Validator.TryValidateObject(request, context, validationResults, validateAllProperties: true))
    {
        var errors = validationResults.ToDictionary(
            v => v.MemberNames.FirstOrDefault() ?? "error",
            v => new[] { v.ErrorMessage ?? "Invalid value." });
        return Results.ValidationProblem(errors);
    }

    try
    {
        var stock = await stockService.CreateStockAsync(request);
        return Results.Created($"/api/stocks/{stock.Symbol}", stock);
    }
    catch (InvalidOperationException ex)
    {
        return Results.Conflict(new { error = ex.Message });
    }
})
.WithName("CreateStock");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

