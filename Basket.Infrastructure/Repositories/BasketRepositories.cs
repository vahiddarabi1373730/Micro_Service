using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Repositories;

public class BasketRepositories(IDistributedCache redis):IBasketRepositories
{
    public async Task<ShoppingCart?> GetBasket(string userName)
    {
        var basket=await redis.GetStringAsync(userName);
        return string.IsNullOrEmpty(basket) ? null : JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart?> CreateBasket(string userName,ShoppingCart basket)
    {
        await redis.SetStringAsync(userName,JsonConvert.SerializeObject(basket));
        return basket;
    }

    public async Task<ShoppingCart?> UpdateBasket(string userName,ShoppingCart basket)
    {
        await redis.SetStringAsync(userName,JsonConvert.SerializeObject(basket));
        var updatedBasket =await GetBasket(userName);
        return updatedBasket;
    }

    public async Task<bool> DeleteBasket(string userName)
    {
        await redis.RemoveAsync(userName);
        return true;
    }
}