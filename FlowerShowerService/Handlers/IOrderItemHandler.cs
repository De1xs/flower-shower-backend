namespace FlowerShowerService.Handlers;

using FlowerShowerService.Data.Entities;
using FlowerShowerService.Models;

public interface IOrderItemHandler
{
    public Task<OrderItem> HandleCreation(OrderItemModel model);

    public Task<OrderItem?> HandleRead(int id);
}
