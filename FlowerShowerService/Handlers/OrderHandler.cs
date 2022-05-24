namespace FlowerShowerService.Handlers;

using FlowerShowerService.Data;
using FlowerShowerService.Data.Entities;
using FlowerShowerService.Models;
using Microsoft.EntityFrameworkCore;

public class OrderHandler : IOrderHandler
{
    private readonly DataContext _db;

    public OrderHandler(DataContext db)
    {
        _db = db;
    }

}
