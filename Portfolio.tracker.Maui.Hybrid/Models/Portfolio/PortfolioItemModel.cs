namespace Portfolio.tracker.Maui.Hybrid.Models.Portfolio;

internal sealed class PortfolioItemModel
{
    public required string UserId { get; set; }
    public required string Name { get; set; }
    public required string Ticker { get; set; }
    public required string PortfolioId { get; set; }
    public string? Logo { get; set; }
}
