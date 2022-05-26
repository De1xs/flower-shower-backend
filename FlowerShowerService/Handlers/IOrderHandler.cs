namespace FlowerShowerService.Handlers;

using FlowerShowerService.Data.Entities;
using FlowerShowerService.Models;

public interface IOrderHandler
{
    Task<Order> GetActiveOrder(User user);
    

    Task<List<Order>> HandleReadOrderAll(User user);

    Task<Order?> HandleWriteOrderItem(User user, int productId, int quantity);

    Task<Order> HandleDeleteOrderItem(User user, int productId);

    Task StartOrder(OrderModel order);
}
