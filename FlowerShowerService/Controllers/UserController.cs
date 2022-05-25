namespace FlowerShowerService.Controllers;

using Data.Entities;
using FlowerShowerService.Handlers;
using FlowerShowerService.Models;
using FlowerShowerService.Security;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/API/[controller]")]
public sealed class UserController : ControllerBase
{
    private readonly IUserHandler _handler;

    public UserController(IUserHandler handler)
    {
        _handler = handler;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromServices] IPasswordHelper passwordVerificationHelper, [FromBody] UserModel userInfo)
    {
        if (await _handler.HandleRead(userInfo) is not null)
        {
            return BadRequest("User with this username already exists, try logging in");
        }

        if (!passwordVerificationHelper.VerifyPassword(userInfo.Password))
        {
            return BadRequest("Password too weak");
        }

        var createdUser = await _handler.HandleCreation(userInfo);

        return Created($"API/user/{createdUser.Id}", createdUser);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserModel userInfo)
    {
        var user = await _handler.HandleRead(userInfo);

        if (user is null)
        {
            return NotFound("User with this username does not exist, please register");
        }

        if(user.Password != userInfo.Password)
        {
            return BadRequest("Incorrect password");
        }

        return Ok(user.Id);
    }

    [HttpGet("{id:int}/Order")]
    public async Task<ActionResult<Order>> ReadOrder(int id)
    {
        var order = await _handler.HandleReadOrder(id);

        if (order == null) return NotFound();
        return order;
    }

    [HttpGet("{id:int}/Orders")]
    public async Task<ActionResult<List<Order>>> ReadOrders(int id)
    {
        var order = await _handler.HandleReadOrderAll(id);

        if (order == null) return NotFound();
        return order;
    }

    [HttpPost("{id:int}/OrderItem/{productId:int}")]
    public async Task<ActionResult<Order>> WriteOrderItem(int id, int productId, [FromQuery]int quantity = 1)
    {
        var order = await _handler.HandleWriteOrderItem(id, productId, quantity);

        if (order == null) return NotFound();
        return order;
    }

    [HttpDelete("{id:int}/OrderItem/{productId:int}")]
    public async Task<ActionResult<Order>> DeleteOrderItem(int id, int productId)
    {
        var order = await _handler.HandleDeleteOrderItem(id, productId);

        if (order == null) return NotFound();
        return order;
    }
}
