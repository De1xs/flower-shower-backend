namespace FlowerShowerService.Controllers;

using FlowerShowerService.Handlers;
using FlowerShowerService.OurExceptions;
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

    [HttpPost("startOrder")]
    public async Task<IActionResult> StartOrder(OrderModel startOrderRequest)
    {
        try
        {
            await _handler.StartOrder(startOrderRequest);
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound("Cart not found");
        }
        catch(CartEmptyException ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }
}
