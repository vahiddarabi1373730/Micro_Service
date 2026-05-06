using AutoMapper;
using Discount.Application.Protos.discount.proto;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Application;

public class CreateDiscountQuery(CouponModel couponModel) : IRequest<CouponModel>
{
    public CouponModel CouponModel { get; set; } = couponModel;
}

public class CreateDiscountQueryHandler(IDiscountRepositories repositories, IMapper mapper)
    : IRequestHandler<CreateDiscountQuery, CouponModel>
{
    public async Task<CouponModel> Handle(CreateDiscountQuery request, CancellationToken cancellationToken)
    {
        var coupon = mapper.Map<Coupon>(request.CouponModel);
        if (await repositories.CreateDiscount(coupon))
        {
            return mapper.Map<CouponModel>(coupon);
        }

        throw new RpcException(new Status(StatusCode.Unknown, "can not create discount"));
    }
}