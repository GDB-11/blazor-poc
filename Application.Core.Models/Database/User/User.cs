using Application.Core.Models.Constants.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Core.Models.Database.User;

[Table("User", Schema = "main")]
public sealed class User
{
    [Key]
    public required string UserId { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public Status.Code Status { get; set; }
    public string? ProfilePicture { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
