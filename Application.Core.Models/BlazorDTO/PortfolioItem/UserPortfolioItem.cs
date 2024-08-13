using Application.Core.Models.Constants.Portfolio;

namespace Application.Core.Models.BlazorDTO.PortfolioItem;

public readonly record struct UserPortfolioItem
{
    public required string PortfolioItemId { get; init; }
    public required string AssetId { get; init; }
    public required string Name { get; init; }
    public required string Ticker { get; init; }
    public string? Logo { get; init; }
    public ItemStatus.Code Display { get; init; }
}
