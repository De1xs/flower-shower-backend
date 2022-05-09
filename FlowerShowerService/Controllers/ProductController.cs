namespace FlowerShowerService.Controllers;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

[ApiController]
[Route("/API/[controller]")]
public class ProductController : ControllerBase
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public ProductController(DataContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create(ProductModel model)
    {
        var product = _mapper.Map<Product>(model);
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> ReadAll()
    {
        var products = await _db.Products.ToListAsync();
        return Ok(products);
    }
}
