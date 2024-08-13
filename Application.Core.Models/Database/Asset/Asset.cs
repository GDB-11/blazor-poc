using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Core.Models.Database.Asset;

[Table("Asset", Schema = "main")]
public sealed class Asset
{
    [Key]
    public required string AssetId { get; set; }
    public required string Name { get; set; }
    public required string Ticker { get; set; }
    public string? Logo { get; set; }
    public required DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
