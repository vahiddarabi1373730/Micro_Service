using Basket.Core.Entities;

namespace Basket.Core.Repositories;

public interface IBasketRepositories
{
    Task<ShoppingCart?> GetBasket(string userName);
    Task<ShoppingCart?> CreateBasket(string userName,ShoppingCart basket);
    Task<ShoppingCart?> UpdateBasket(string userName,ShoppingCart basket);
    Task<bool> DeleteBasket(string userName);
}