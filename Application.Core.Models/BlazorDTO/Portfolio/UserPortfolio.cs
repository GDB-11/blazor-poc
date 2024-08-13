using Application.Core.Models.Constants.Portfolio;

namespace Application.Core.Models.BlazorDTO.Portfolio;

public readonly record struct UserPortfolio
{
    public required string PortfolioId { get; init; }
    public required string UserId { get; init; }
    public required string Name { get; init; }
    public Status.Code Status { get; init; }
}
