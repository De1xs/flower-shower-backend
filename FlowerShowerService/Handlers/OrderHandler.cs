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
    private const string ProductIdNotFound = "Product with specified id does not exist";

    public OrderHandler(DataContext db, IProductHandler productHandler)
    {
        _db = db;
        _productHandler = productHandler;
    }

    public async Task<Order> GetCreatedActiveOrder(User user)
    {
        var order = await _db.Orders.Include(x => x.OrderItems).ThenInclude(ot => ot.Product)
            .SingleOrDefaultAsync(o => o.User.Id == user.Id && !o.Completed);
        // Create new Active Order if none exists
        if(order is null)
        {
            order = new Order() { User = user, OrderItems = new List<OrderItem>(), Completed = false };
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
        }

        return order;
    }

    public async Task<List<Order>> HandleReadOrderAll(int userId)
    {
        return await _db.Orders.Where(o => o.User.Id == userId).ToListAsync();
    }

    // Deletes OrderItem from Order
    // Creates new Order if no Active Order exists
    public async Task<Order> HandleDeleteOrderItem(User user, int productId)
    {
        var order = await GetCreatedActiveOrder(user);
        var product = await _productHandler.HandleRead(productId);
        if (product is null)
            throw new KeyNotFoundException(ProductIdNotFound);
        var orderItem = order.OrderItems.SingleOrDefault(i => i.Product.Id == productId);
        if (orderItem is null)
            return order;

        order.OrderItems.Remove(orderItem);
        product.UnitsInStock += orderItem.Quantity;
        await _db.SaveChangesAsync();
        return order;
    }

    // Adds OrderItem to Order
    // Creates new Order if no Active Order exists
    public async Task<Order> HandleWriteOrderItem(User user, int productId, int quantity)
    {
        var product = await _productHandler.HandleRead(productId);
        if (product is null)
            throw new KeyNotFoundException(ProductIdNotFound);

        if (product.UnitsInStock < quantity)
            throw new NotInStockException("Quantity exceeds items left in stock");

        // Override existing Order Items with same ProductId
        var order = await HandleDeleteOrderItem(user, productId);
        order.OrderItems.Add(new OrderItem() { Product = product, Quantity = quantity });
        product.UnitsInStock -= quantity;
        await _db.SaveChangesAsync();

        return order;
    }

    public async Task StartOrder(OrderModel orderStartRequest)
    {
        var order = await _db.Orders.Include(o => o.OrderItems).SingleOrDefaultAsync(o => o.User.Id == orderStartRequest.UserId && o.Completed == false);

        // If Order is empty, that means no Order Items were added
        if (order is null || !order.OrderItems.Any())
        {
            throw new CartEmptyException("Cart is empty");
        }

        order.Address = orderStartRequest.Address;
        order.OrderedOn = DateTime.Now;
        order.DeliveryDate = orderStartRequest.DeliveryData;
        order.Completed = true;

        await _db.SaveChangesAsync();
    }
}
