namespace FlowerShowerService.Models;

using AutoMapper;
using Data.Entities;

public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<Product, ProductModel>()
            .ReverseMap();
    }
}
