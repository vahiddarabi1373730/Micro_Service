namespace Basket.Core.Entities;

public class BasketCheckout
{
    public Guid Guid { get; set; }
    public string Username { get; set; }
    
    //Customer
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    
    
    //Address
    public string AddressLine { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    
    
    //Payment
    public decimal TotalPrice { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string PaymentTrackingCode { get; set; }
    
    //Shipping
    public string TrackingCode { get; set; }
    public string ShippingTrackingCode { get; set; }
}