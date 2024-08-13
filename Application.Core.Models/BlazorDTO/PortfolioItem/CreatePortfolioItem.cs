namespace Application.Core.Models.BlazorDTO.PortfolioItem;

public readonly record struct CreatePortfolioItem
{
    public required string Name { get; init; }
    public required string Ticker { get; init; }
    public required string PortfolioId { get; init; }
}
