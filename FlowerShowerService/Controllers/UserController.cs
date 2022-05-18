namespace FlowerShowerService.Controllers;

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
}
