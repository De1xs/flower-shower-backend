namespace FlowerShowerService.Handlers;

using FlowerShowerService.Data.Entities;
using FlowerShowerService.Models;

public interface IOrderHandler
{
    Task<Order> GetActiveOrder(int userId);

    Order CreateNewOrder(User user);

    Task<List<Order>> HandleReadOrderAll(int userId);

    Task<Order?> HandleWriteOrderItem(int userId, int productId, int quantity);

    Task<Order> HandleDeleteOrderItem(int userId, int productId);

    Task StartOrder(OrderModel order);
}
