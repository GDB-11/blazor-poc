using LiteDB;
using static Application.Core.Models.Constants.PortfolioItem.Constants;

namespace Application.Core.Models.Database.Transactions;

public sealed class Transaction
{
    [BsonId]
    public required string TransactionId { get; set; }
    public required string PortfolioId { get; set; }
    public required string PortfolioItemId { get; set; }
    public required string AssetId { get; set; }
    public OperationType OperationType { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public string? TransactionFeeAssetId { get; set; }
    public decimal TransactionFeeQuantity { get; set; }
    public decimal TransactionFeeAssetPrice { get; set; }
    public required DateTime TransactionDateTime { get; set; }
    public string? TransactionHash { get; set; }
    public string? TransactionUrl { get; set; }
    public required DateTime CreationDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
