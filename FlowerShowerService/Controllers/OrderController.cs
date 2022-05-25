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
/* TODO add Handler
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var order = await _handler.HandleDelete(id);

        if (order == null) return NotFound();

        return NoContent();
    }
    */
}
