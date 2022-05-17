namespace FlowerShowerService.Controllers;

using FlowerShowerService.Data;
using FlowerShowerService.Data.Entities;
using FlowerShowerService.Models;
using FlowerShowerService.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("/API/user")]
public sealed class UserController : ControllerBase
{
    private readonly DataContext _db;

    public UserController(DataContext db)
    {
        _db = db;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromServices] IPasswordHelper passwordVerificationHelper, [FromBody] UserModel userInfo)
    {
        if (await _db.Users.SingleOrDefaultAsync(u => u.Username == userInfo.Username) is not null)
        {
            return BadRequest("User with this username already exists, try logging in");
        }

        if (!passwordVerificationHelper.VerifyPassword(userInfo.Password))
        {
            return BadRequest("Password too weak");
        }

        User newUser = new()
        {
            Username = userInfo.Username,
            Password = userInfo.Password
        };

        _db.Users.Add(newUser);

        await _db.SaveChangesAsync();

        return Ok(newUser.Id);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserModel userInfo)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u => u.Username == userInfo.Username);

        if (user is null)
        {
            return BadRequest("User with this username does not exist, please register");
        }

        if(user.Password != userInfo.Password)
        {
            return BadRequest("Incorrect password");
        }

        return Ok(user.Id);
    }
}
