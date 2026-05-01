namespace Basket.Application.Response;

public class ShoppingCartItemResponse
{
    public int Quantity { get; set; } 
    public string  ProductId { get; set; }
    public string ImageFile { get; set; }
    public string ProductName { get; set; }
    public int Price { get; set; }
}