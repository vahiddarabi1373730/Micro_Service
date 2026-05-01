using AutoMapper;
using Basket.Application.Response;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Commands;

public class UpdateBasketCommand(string userName,List<ShoppingCartItem> items) : IRequest<ShoppingCartResponse>
{
    public string UserName { get; set; } = userName;
    public List<ShoppingCartItem> Items { get; set; } = items;
}

public class UpdateBasketCommandHandler(IBasketRepositories repositories, IMapper mapper)
    : IRequestHandler<UpdateBasketCommand, ShoppingCartResponse>
{
    public async Task<ShoppingCartResponse> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = mapper.Map<ShoppingCart>(request);
        var sh=await repositories.UpdateBasket(request.UserName, basket);
        return mapper.Map<ShoppingCartResponse>(sh);
    }
}