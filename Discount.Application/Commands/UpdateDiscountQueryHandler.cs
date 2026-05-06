using AutoMapper;
using Discount.Application.Protos.discount.proto;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Application;

public class UpdateDiscountQuery(CouponModel couponModel) : IRequest<CouponModel>
{
    public CouponModel CouponModel { get; set; } = couponModel;
}

public class UpdateDiscountQueryHandler(IDiscountRepositories repositories, IMapper mapper)
    : IRequestHandler<UpdateDiscountQuery, CouponModel>
{
    public async Task<CouponModel> Handle(UpdateDiscountQuery request, CancellationToken cancellationToken)
    {
        var coupon = mapper.Map<Coupon>(request.CouponModel);
        if (await repositories.UpdateDiscount(request.CouponModel.Id, coupon))
        {
            return mapper.Map<CouponModel>(coupon);
        }

        throw new RpcException(new Status(StatusCode.Unknown, "can not Update discount"));
    }
}