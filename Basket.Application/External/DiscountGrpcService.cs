using Discount.Application.Protos.discount.proto;

namespace Basket.Application.External;

public class DiscountGrpcService(DiscountProtoServices.DiscountProtoServicesClient client)
{
    public async Task<CouponModel> GetDiscountByProductName(string productName)
    {

        var discount = new GetDiscountByProductNameRequest()
        {
            ProductName = productName
        };
        var result = await client.GetDiscountByProductNameAsync(discount);
        return result;
    }

    public async Task<CouponModel> GetDiscountByProductId(string productId)
    {
        var discount = new GetDiscountByProductIdRequest()
        {
            ProductId = productId
        };
        return await client.GetDiscountByProductIdAsync(discount);
    }
}