namespace FlowerShowerService.Data.Entities;

using Microsoft.EntityFrameworkCore;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Category Category { get; set; } = Category.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageLink { get; set; } = string.Empty;

    [Precision(5, 2)]
    public decimal Price { get; set; }

    public int UnitsInStock { get; set; }
}
