namespace FlowerShowerService.Handlers;

using FlowerShowerService.Data.Entities;
using FlowerShowerService.Models;

public interface IProductHandler
{
    public Task<Product> HandleCreation(ProductModel model);

    public Task<Product?> HandleRead(int id);

    public Task<List<Product>> HandleReadAll(Category category);
}
