namespace FlowerShowerService.Controllers;

using Data.Entities;
using FlowerShowerService.Handlers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;

[ApiController]
[EnableCors]
[Route("/API/[controller]")]
public class OrderItemController : ControllerBase
{
    private readonly IOrderItemHandler _handler;

    public OrderItemController(IOrderItemHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<ActionResult<OrderItem>> Create(OrderItemModel model)
    {
        var createdOrderItem = await _handler.HandleCreation(model);

        return Created($"API/Order/{createdOrderItem.Id}", createdOrderItem);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderItem>> Read(int id)
    {
        var orderItem = await _handler.HandleRead(id);

        if (orderItem == null) return NotFound();
        return orderItem;
    }
}
