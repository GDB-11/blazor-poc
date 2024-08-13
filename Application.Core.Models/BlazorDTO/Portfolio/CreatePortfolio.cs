namespace Application.Core.Models.BlazorDTO.Portfolio;

public readonly record struct CreatePortfolio
{
    public required string UserId { get; init; }
    public required string Name { get; init; }
}
