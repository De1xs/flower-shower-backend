namespace FlowerShowerService.Handlers;

using FlowerShowerService.Data;
using FlowerShowerService.Data.Entities;
using FlowerShowerService.Models;
using Microsoft.EntityFrameworkCore;

public class ProductHandler : IProductHandler
{
    private readonly DataContext _db;

    public ProductHandler(DataContext db)
    {
        _db = db;
    }

    public async Task<Product> HandleCreation(ProductModel model)
    {
        var product = new Product
        {
            Name = model.Name,
            Category = model.Category,
            Description = model.Description,
            ImageLink = model.ImageLink,
            Price = model.Price
        };

        var created = _db.Add(product);
        await _db.SaveChangesAsync();

        return created.Entity;
    }

    public async Task<Product?> HandleRead(int id)
    {
        return await _db.Products.SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product>> HandleReadAll(Category category)
    {
        return await _db.Products.Where(p => p.Category == category).ToListAsync();
    }
}
