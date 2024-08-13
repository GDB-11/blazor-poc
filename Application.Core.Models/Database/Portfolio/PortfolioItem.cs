using Application.Core.Models.Constants.Portfolio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Core.Models.Database.Portfolio;

[Table("PortfolioItem", Schema = "main")]
public sealed class PortfolioItem
{
    [Key]
    public required string PortfolioItemId { get; set; }
    public required string PortfolioId { get; set; }
    public required string AssetId { get; set; }
    public ItemStatus.Code Display { get; set; }
    public required DateTime CreatedDate { get; set; }
}
