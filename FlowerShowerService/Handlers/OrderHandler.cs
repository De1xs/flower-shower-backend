namespace FlowerShowerService.Handlers;

using FlowerShowerService.Data;
using FlowerShowerService.Data.Entities;
using FlowerShowerService.Models;
using FlowerShowerService.OurExceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class OrderHandler : IOrderHandler
{
    private readonly DataContext _db;
    private readonly IProductHandler _productHandler;

    public OrderHandler(DataContext db, IProductHandler productHandler)
    {
        _db = db;
        _productHandler = productHandler;
    }

    public async Task<Order> GetActiveOrder(User user)
    {
        var order = await _db.Orders.Include(x => x.OrderItems).SingleOrDefaultAsync(o => o.User.Id == user.Id && !o.Completed);
        if (order != null)
            return order;
        return new Order() { User = user, OrderItems = new List<OrderItem>() };
    }

    public async Task<List<Order>> HandleReadOrderAll(User user)
    {
        return await _db.Orders.Where(o => o.User.Id == user.Id).ToListAsync();
    }

    public async Task<Order> HandleDeleteOrderItem(User user, int productId)
    {
        var order = await GetActiveOrder(user);
        order.OrderItems.RemoveAll(i => i.Product.Id == productId);
        await _db.SaveChangesAsync();
        return order;
    }

    public async Task<Order?> HandleWriteOrderItem(User user, int productId, int quantity)
    {
        try
        {
            var order = await GetActiveOrder(user);
            var product = await _productHandler.HandleRead(productId);
            if (product is null)
                throw new KeyNotFoundException();

            order.OrderItems.Add(new OrderItem() { Product = product, Quantity = quantity });
            await _db.SaveChangesAsync();

            return order;
        }
        catch (KeyNotFoundException exception)
        {
            return null;
        }
    }

    public async Task StartOrder(OrderModel orderStartRequest)
    {
        var order = await _db.Orders.Include(o => o.OrderItems).SingleOrDefaultAsync(o => o.User.Id == orderStartRequest.UserId);

        if(order is null)
        {
            throw new KeyNotFoundException("There is no order");
        }

        if (!order.OrderItems.Any())
        {
            throw new CartEmptyException("Cart is empty");
        }

        order.Address = orderStartRequest.Address;
        order.OrderedOn = DateTime.Now;
    }
}
