namespace FlowerShowerService.Controllers;

using Data;
using Data.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

[ApiController]
[EnableCors]
[Route("/API/[controller]")]
public class ProductController : ControllerBase
{
    private readonly DataContext _db;

    public ProductController(DataContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create(ProductModel model)
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

        return Created($"API/Product/{created.Entity.Id}", created.Entity);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> Read(int id)
    {
        var product = await _db.Products.FindAsync(id);

        if (product == null) return NotFound();
        return product;
    }

    [HttpGet("{category}")]
    public async Task<ActionResult<IEnumerable<Product>>> ReadAll(Category category)
    {
        var products = await _db.Products.Where(p => p.Category == category).ToListAsync();
        return Ok(products);
    }
}
