using System.ComponentModel.DataAnnotations;

namespace StockAnalyst.Core.Models;

/// <summary>
/// Request model for creating a new stock.
/// </summary>
public class CreateStockRequest
{
    /// <summary>Gets or sets the stock ticker symbol (required, unique).</summary>
    [Required(ErrorMessage = "Symbol is required.")]
    [StringLength(10, MinimumLength = 1, ErrorMessage = "Symbol must be between 1 and 10 characters.")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>Gets or sets the stock name (required).</summary>
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 200 characters.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>Gets or sets the initial price.</summary>
    [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative.")]
    public decimal Price { get; set; }
}
