namespace FlowerShowerService.Controllers;

using Data.Entities;
using FlowerShowerService.Handlers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;

[ApiController]
[EnableCors]
[Route("/API/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderHandler _handler;

    public OrderController(IOrderHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<ActionResult<Order>> Create(OrderModel model)
    {
        var createdOrder = await _handler.HandleCreation(model);

        return Created($"API/Order/{createdOrder.Id}", createdOrder);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Order>> Read(int id)
    {
        var order = await _handler.HandleRead(id);

        if (order == null) return NotFound();
        return order;
    }
}
