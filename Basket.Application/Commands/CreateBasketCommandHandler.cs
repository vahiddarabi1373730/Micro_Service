using AutoMapper;
using Basket.Application.External;
using Basket.Application.Response;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Commands;

public class CreateBasketCommand(string userName,List<ShoppingCartItem> items) : IRequest<ShoppingCartResponse>
{
    public string UserName { get; set; } = userName;
    public List<ShoppingCartItem> Items { get; set; } = items;
}

public class CreateBasketCommandHandler(IBasketRepositories repositories, IMapper mapper,DiscountGrpcService discountGrpcService)
    : IRequestHandler<CreateBasketCommand, ShoppingCartResponse>
{
    public async Task<ShoppingCartResponse> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.Items)
        {
            var discount = await discountGrpcService.GetDiscountByProductName(item.ProductName);
            item.Price -= discount.Amount;
        }
        var basket = mapper.Map<ShoppingCart>(request);
        var sh=await repositories.CreateBasket(request.UserName, basket);
        return mapper.Map<ShoppingCartResponse>(sh);
    }
}