namespace FlowerShowerService.Handlers;

using FlowerShowerService.Data.Entities;
using FlowerShowerService.Models;

public interface IUserHandler
{
    public Task<User> HandleCreation(UserModel userInfo);
    public Task<User?> HandleRead(UserModel userInfo);
    public Task<User?> HandleRead(int userId);
    
    
}
