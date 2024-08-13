using Application.Core.Models.Constants.Portfolio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Core.Models.Database.Portfolio;

[Table("Portfolio", Schema = "main")]
public sealed class Portfolio
{
    [Key]
    public required string PortfolioId { get; set; }
    public required string UserId { get; set; }
    public required string Name { get; set; }
    public Status.Code Status { get; set; }
    public required DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
