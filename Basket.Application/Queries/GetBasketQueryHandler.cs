using AutoMapper;
using Basket.Application.Response;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Queries;
public class GetBasketQuery(string userName):IRequest<ShoppingCartResponse>
{
    public string UserName { get; set; } = userName;

}
public class GetBasketQueryHandler(IBasketRepositories repositories,IMapper mapper):IRequestHandler<GetBasketQuery,ShoppingCartResponse>
{
    public async Task<ShoppingCartResponse> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
       var basket= await repositories.GetBasket(request.UserName);
       return basket is null ? new ShoppingCartResponse(request.UserName):mapper.Map<ShoppingCartResponse>(basket);
    }
}