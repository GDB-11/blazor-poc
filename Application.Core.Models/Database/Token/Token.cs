namespace Application.Core.Models.Database.Token;

public sealed class Token
{
    public Guid TokenId { get; set; }
    public required string Name { get; set; }
    public required string Ticker { get; set; }
    public long CirculatingSupply { get; set; }
    public Guid BlockchainId { get; set; }
    public string? Logo { get; set; }
}
