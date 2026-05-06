using Discount.Core.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Commands;

public class DeleteDiscountByProductNameQuery(string productName) : IRequest<bool>
{
    public string ProductName { get; set; } = productName;
}

public class DeleteDiscountByProductNameQueryHandler(IDiscountRepositories repositories)
    : IRequestHandler<DeleteDiscountByProductNameQuery, bool>
{
    public async Task<bool> Handle(DeleteDiscountByProductNameQuery request, CancellationToken cancellationToken)
    {
        return await repositories.DeleteDiscountByName(request.ProductName)
            ? true
            : throw new RpcException(new Status(StatusCode.Unknown, "can not Delete discount"));
    }
}