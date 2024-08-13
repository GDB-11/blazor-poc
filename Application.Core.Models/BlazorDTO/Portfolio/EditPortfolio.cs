namespace Application.Core.Models.BlazorDTO.Portfolio;

public readonly record struct EditPortfolio
{
    public required string PortfolioId { get; init; }
    public required string Name { get; init; }
}
