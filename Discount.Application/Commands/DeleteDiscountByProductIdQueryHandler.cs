using Discount.Core.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Commands;

public class DeleteDiscountByProductIdQuery(string productId) :IRequest<bool>
{
    public string ProductId { get; set; } = productId;
}

public  class DeleteDiscountByProductIdQueryHandler(IDiscountRepositories repositories):IRequestHandler<DeleteDiscountByProductIdQuery,bool>
{
    public async Task<bool> Handle(DeleteDiscountByProductIdQuery request, CancellationToken cancellationToken)
    {
        return await repositories.DeleteDiscount(request.ProductId)
            ? true
            : throw new RpcException(new Status(StatusCode.Unknown, "can not Delete discount"));
    }
}