using Discount.Core.Entities;

namespace Discount.Core.Repositories;

public interface IDiscountRepositories
{
    Task<Coupon> GetDiscountById(string productId);
    Task<Coupon> GetDiscountByName(string productName);
    Task<bool> CreateDiscount(Coupon coupon);
    Task<bool> UpdateDiscount(int id,Coupon coupon);
    Task<bool> DeleteDiscount(string productId);
    Task<bool> DeleteDiscountByName(string productName);
}