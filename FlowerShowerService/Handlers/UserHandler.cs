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

    public async Task<User?> HandleRead(int userId)
    {
        return await _db.Users.SingleOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<Order> HandleReadOrder(int userId)
    {
        return await GetActiveOrder(userId);
    }

    public async Task<List<Order>> HandleReadOrderAll(int userId)
    {
        return await _db.Orders.Where(o => o.User.Id == userId).ToListAsync();
    }

    public async Task<Order?> HandleWriteOrderItem(int userId, int productId, int quantity)
    {
        try
        {
            var order = await GetActiveOrder(userId);
            var product = await GetProduct(productId);
            order.OrderItems.Add(new OrderItem() { Product = product, Quantity = quantity });
            await _db.SaveChangesAsync();
            return order;
        }
        catch (KeyNotFoundException exception)
        {
            return null;
        }
    }

    public async Task<Order> HandleDeleteOrderItem(int userId, int productId)
    {
        var order = await GetActiveOrder(userId);
        order.OrderItems.RemoveAll(i => i.Product.Id == productId);
        await _db.SaveChangesAsync();
        return order;
    }

    private async Task<Order> GetActiveOrder(int userId)
    {
        var order = await _db.Orders.Include(x => x.OrderItems).SingleOrDefaultAsync(o => o.User.Id == userId && !o.Completed);
        if (order != null)
            return order;
        var user = await GetUser(userId);
        return new Order() { User = user, OrderItems = new List<OrderItem>() };
    }

    private async Task<User> GetUser(int userId)
    {
        var user = await _db.Users.FindAsync(userId);
        if (user != null)
            return user;
        throw new KeyNotFoundException();
    }

    private async Task<Product> GetProduct(int productId)
    {
        var product = await _db.Products.FindAsync(productId);
        if (product != null)
            return product;
        throw new KeyNotFoundException();
    }
}
