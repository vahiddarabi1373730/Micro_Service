using AutoMapper;
using Discount.Application.Protos.discount.proto;
using Discount.Core.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Queries;


public class GetDiscountByProductNameQuery(string productName):IRequest<CouponModel>
{
    public string ProductName { get; set; } = productName;
}
public class GetDiscountByProductNameQueryHandler(IDiscountRepositories repositories,IMapper mapper):IRequestHandler<GetDiscountByProductNameQuery,CouponModel>
{
    public async Task<CouponModel> Handle(GetDiscountByProductNameQuery request, CancellationToken cancellationToken)
    {
        var discount =await repositories.GetDiscountByName(request.ProductName);
        if (discount == null)
        {
            throw new  RpcException(new Status(StatusCode.NotFound,"Discount not found"));
        }
        return mapper.Map<CouponModel>(discount);
    }
}