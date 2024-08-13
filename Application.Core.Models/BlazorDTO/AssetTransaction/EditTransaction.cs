using static Application.Core.Models.Constants.PortfolioItem.Constants;

namespace Application.Core.Models.BlazorDTO.AssetTransaction;

public readonly record struct EditTransaction
{
    public required string TransactionId { get; init; }
    public OperationType OperationType { get; init; }
    public decimal Price { get; init; }
    public decimal Quantity { get; init; }
    public string TransactionFeeAssetId { get; init; }
    public decimal TransactionFeeQuantity { get; init; }
    public decimal TransactionFeeAssetPrice { get; init; }
    public required DateTime TransactionDateTime { get; init; }
    public string? TransactionHash { get; init; }
    public string? TransactionUrl { get; init; }
}
