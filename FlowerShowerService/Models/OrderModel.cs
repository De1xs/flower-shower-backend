namespace FlowerShowerService.Models;

using System.ComponentModel.DataAnnotations;
using Data.Entities;

public class OrderModel
{
    [Required]
    public bool Completed { get; set; } = false;

    [Required]
    public DateTime OrderedOn { get; set; } = DateTime.Now;

    [Required]
    public User User { get; set; } = new();

    [Required]
    public List<OrderItem> OrderItems { get; set; } = new();

    [Required]
    public string Address { get; set; } = string.Empty;
}
