namespace FlowerShowerService.Handlers;

using FlowerShowerService.Data.Entities;
using FlowerShowerService.Models;

public interface IUserHandler
{
    public Task<User> HandleCreation(UserModel userInfo);
    public Task<User?> HandleRead(UserModel userInfo);
    public Task<User?> HandleRead(int userId);
    public Task<Order> HandleReadOrder(int userId);
    public Task<List<Order>> HandleReadOrderAll(int userId);
    public Task<Order?> HandleWriteOrderItem(int userId, int productId, int quantity);
    public Task<Order> HandleDeleteOrderItem(int userId, int productId);
}
