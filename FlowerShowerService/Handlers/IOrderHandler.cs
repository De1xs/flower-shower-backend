namespace FlowerShowerService.Handlers;

using FlowerShowerService.Data.Entities;
using FlowerShowerService.Models;

public interface IOrderHandler
{
    Task<Order> GetCreatedActiveOrder(User user);
    Task<List<Order>> HandleReadOrderAll(int userId);
    Task<Order> HandleWriteOrderItem(User user, int productId, int quantity);
    Task<Order> HandleDeleteOrderItem(User user, int productId);
    Task StartOrder(OrderModel order);
}
