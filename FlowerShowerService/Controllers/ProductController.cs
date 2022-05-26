namespace FlowerShowerService.Controllers;

using Data.Entities;
using FlowerShowerService.Handlers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;

[ApiController]
[EnableCors]
[Route("/API/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductHandler _handler;

    public ProductController(IProductHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create(ProductModel model)
    {
        var createdProduct = await _handler.HandleCreation(model);

        return Created($"API/Product/{createdProduct.Id}", createdProduct);
    }

    [HttpGet("{productId:int}")]
    public async Task<ActionResult<Product>> Read(int productId)
    {
        var product = await _handler.HandleRead(productId);

        if (product == null) return NotFound();
        return product;
    }

    [HttpGet("{category}")]
    public async Task<ActionResult<IEnumerable<Product>>> ReadAll(Category category)
    {
        var products = await _handler.HandleReadAll(category);
        return Ok(products);
    }
}
