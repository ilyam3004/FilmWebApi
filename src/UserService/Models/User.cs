using System.ComponentModel.DataAnnotations;

namespace UserService.Models;

public class User
{
    [Key]
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string Login { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}