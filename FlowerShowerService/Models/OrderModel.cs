using System.ComponentModel.DataAnnotations;

namespace FlowerShowerService.Models;

public class OrderModel
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public string Address { get; set; } = string.Empty;

    //We should probably add card info here as well :/
}
