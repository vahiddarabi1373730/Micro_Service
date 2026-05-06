using Discount.Application;
using Discount.Application.Commands;
using Discount.Application.Protos.discount.proto;
using Discount.Application.Queries;
using Grpc.Core;
using MediatR;

namespace Discount.Api.Services;

public class DiscountService(IMediator mediator) : DiscountProtoServices.DiscountProtoServicesBase
{
    public override async Task<CouponModel> CreateDisCount(CreateDiscountRequest request, ServerCallContext context)
    {
        var command = new CreateDiscountQuery(request.CouponModel);
        return await mediator.Send(command);
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var command = new UpdateDiscountQuery(request.CouponModel);
        return await mediator.Send(command);
    }

    public override async Task<CouponModel> GetDiscountByProductName(GetDiscountByProductNameRequest request,
        ServerCallContext context)
    {
        var query = new GetDiscountByProductNameQuery(request.ProductName);
        return await mediator.Send(query);
    }

    public override async Task<CouponModel> GetDiscountByProductId(GetDiscountByProductIdRequest request,
        ServerCallContext context)
    {
        var query = new GetDiscountByProductIdQuery(request.ProductId);
        return await mediator.Send(query);
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscountByProductId(
        DeleteDiscountByProductIdRequest request, ServerCallContext context)
    {
        var command = new DeleteDiscountByProductIdQuery(request.ProductId);
        var result = await mediator.Send(command);
        return new DeleteDiscountResponse() { Success = result };
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscountByProductName(
        DeleteDiscountByProductNameRequest request,
        ServerCallContext context)
    {
        var command = new DeleteDiscountByProductNameQuery(request.ProductName);
        var result = await mediator.Send(command);
        return new DeleteDiscountResponse() { Success = result };
    }
}