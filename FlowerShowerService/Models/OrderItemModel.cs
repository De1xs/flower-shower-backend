namespace FlowerShowerService.Models;

using System.ComponentModel.DataAnnotations;
using Data.Entities;

public class OrderItemModel
{
    [Required]
    public Product Product { get; set; } = new();

    [Required]
    public int Quantity { get; set; }
}
