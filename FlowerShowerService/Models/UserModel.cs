namespace FlowerShowerService.Models;

using System.ComponentModel.DataAnnotations;

public class UserModel
{
    [Required]
    [MinLength(5)]
    [MaxLength(20)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MinLength(5)]
    [MaxLength(20)]
    public string Password { get; set; } = string.Empty;
}
