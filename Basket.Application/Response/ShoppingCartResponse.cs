using Basket.Core.Entities;

namespace Basket.Application.Response;

public class ShoppingCartResponse
{
    public Guid Guid { get; set; } 
    public string UserName { get; set; } 
    public List<ShoppingCartItem> Items { get; set; } = [];

    public ShoppingCartResponse()
    {
        
    }
    public ShoppingCartResponse(string userName)
    {
        UserName = userName;
    }
    public decimal CalculateOriginalPrice()
    {
        return Items.Sum(item => item.Quantity * item.Price);
    }
}