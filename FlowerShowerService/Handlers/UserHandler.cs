namespace FlowerShowerService.Handlers;

using FlowerShowerService.Data;
using FlowerShowerService.Data.Entities;
using FlowerShowerService.Models;
using Microsoft.EntityFrameworkCore;

public class UserHandler : IUserHandler
{
    private readonly DataContext _db;

    public UserHandler(DataContext db)
    {
        _db = db;
    }

    public async Task<User> HandleCreation(UserModel userInfo)
    {
        User newUser = new()
        {
            Username = userInfo.Username,
            Password = userInfo.Password
        };

        var created = _db.Users.Add(newUser);

        await _db.SaveChangesAsync();

        return created.Entity;
    }

    public async Task<User?> HandleRead(UserModel userInfo)
    {
        return await _db.Users.SingleOrDefaultAsync(u => u.Username == userInfo.Username);
    }
}
