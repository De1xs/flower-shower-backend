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
    private readonly IUserHandler _userHandler;
    private readonly IOrderHandler _orderHandler;

    public UserController(IUserHandler userHandler, IOrderHandler orderHandler)
    {
        _userHandler = userHandler;
        _orderHandler = orderHandler;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromServices] IPasswordHelper passwordVerificationHelper, [FromBody] UserModel userInfo)
    {
        if (await _userHandler.HandleRead(userInfo) is not null)
        {
            return BadRequest("User with this username already exists, try logging in");
        }

        if (!passwordVerificationHelper.VerifyPassword(userInfo.Password))
        {
            return BadRequest("Password too weak");
        }

        var createdUser = await _userHandler.HandleCreation(userInfo);

        return Created($"API/user/{createdUser.Id}", createdUser);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserModel userInfo)
    {
        var user = await _userHandler.HandleRead(userInfo);

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
    public async Task<ActionResult<Order>> ReadOrder(int userId)
    {
        var user = await _userHandler.HandleRead(userId);
        var order = await _orderHandler.GetActiveOrder(user);

        if (order == null) return NotFound();
        return order;
    }

    [HttpGet("{id:int}/Orders")]
    public async Task<ActionResult<List<Order>>> ReadOrders(int userId)
    {
        var user = await _userHandler.HandleRead(userId);
        var order = await _orderHandler.HandleReadOrderAll(user);

        if (order == null) return NotFound();
        return order;
    }

    [HttpPost("{id:int}/OrderItem/{productId:int}")]
    public async Task<ActionResult<Order>> WriteOrderItem(int userId, int productId, [FromQuery]int quantity = 1)
    {
        var user = await _userHandler.HandleRead(userId);
        var order = await _orderHandler.HandleWriteOrderItem(user, productId, quantity);

        if (order == null) return NotFound();
        return order;
    }

    [HttpDelete("{id:int}/OrderItem/{productId:int}")]
    public async Task<ActionResult<Order>> DeleteOrderItem(int userId, int productId)
    {
        var user = await _userHandler.HandleRead(userId);
        var order = await _orderHandler.HandleDeleteOrderItem(user, productId);

        if (order == null) return NotFound();
        return order;
    }
}
