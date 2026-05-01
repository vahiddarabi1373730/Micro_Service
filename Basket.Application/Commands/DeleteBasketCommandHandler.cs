using AutoMapper;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Commands;

public class DeleteBasketCommand(string userName):IRequest<bool>
{
    public string UserName { get; set; } = userName;
}
public class DeleteBasketCommandHandler(IBasketRepositories repositories,IMapper mapper):IRequestHandler<DeleteBasketCommand,bool>
{
    public async Task<bool> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        await repositories.DeleteBasket(request.UserName);
        return true;
    }
}