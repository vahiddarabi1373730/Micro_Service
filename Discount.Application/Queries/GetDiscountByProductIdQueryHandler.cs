using AutoMapper;
using Discount.Application.Protos.discount.proto;
using Discount.Core.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Queries;


public class GetDiscountByProductIdQuery(string productId):IRequest<CouponModel>
{
    public string ProductId { get; set; } = productId;
}
public class GetDiscountByProductIdQueryHandler(IDiscountRepositories repositories,IMapper mapper):IRequestHandler<GetDiscountByProductIdQuery,CouponModel>
{
    public async Task<CouponModel> Handle(GetDiscountByProductIdQuery request, CancellationToken cancellationToken)
    {
        var discount =await repositories.GetDiscountById(request.ProductId);
        if (discount == null)
        {
            throw new  RpcException(new Status(StatusCode.NotFound,"Discount not found"));
        }
        return mapper.Map<CouponModel>(discount);
    }
}