namespace FlowerShowerService.Models;

using System.ComponentModel.DataAnnotations;
using Data.Entities;

public class ProductModel
{
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string Name { get; set; } = string.Empty;

    public Category Category { get; set; } = Category.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string ImageLink { get; set; } = string.Empty;

    [Required]
    [Range(0.01, 1000)]
    public decimal Price { get; set; }

    
}
