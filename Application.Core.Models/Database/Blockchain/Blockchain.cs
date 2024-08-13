namespace Application.Core.Models.Database.Blockchain;

public sealed class Blockchain
{
    public Guid BlockchainId { get; set; }
    public required string NetworkName { get; set; }
    public int ChainId { get; set; }
    public required string RpcUrl { get; set; }
    public required string CurrencySymbol { get; set; }
    public required string BlockExplorerUrl { get; set; }
}