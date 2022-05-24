namespace FlowerShowerService.Handlers;

using FlowerShowerService.Data.Entities;
using FlowerShowerService.Models;

public interface IOrderHandler
{
    public Task<Order> HandleCreation(OrderModel model);

    public Task<Order?> HandleRead(int id);
}
