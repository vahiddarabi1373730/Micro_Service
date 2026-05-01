using AutoMapper;
using Basket.Application.Commands;
using Basket.Application.Response;
using Basket.Core.Entities;

namespace Basket.Application.Mapper;

public class ProfileMapper:Profile
{
    public  ProfileMapper()
    {
        CreateMap<ShoppingCart,ShoppingCartResponse>().ReverseMap();
        CreateMap<ShoppingCartItem,ShoppingCartItemResponse>().ReverseMap();
        CreateMap<CreateBasketCommand, ShoppingCart>().ReverseMap();
        CreateMap<UpdateBasketCommand, ShoppingCart>().ReverseMap();
    }
}