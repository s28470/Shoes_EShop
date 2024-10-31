namespace Shoes_Eshop_Project.entities;
using System.ComponentModel.DataAnnotations;

public class Customer
{
    
    public string Name { get; set; }
    
    public string ContactNumber { get; set; }
    
    public string? Email { get; set; }

    
    public Address Address { get; set; }

    public decimal TotalPurchases { get; set; } 
    
    private static decimal _totalPurchasesToBecomeVip = 1000;

    private static double _vipDiscount = 0.02;
    
    public CustomerStatus CustomerStatus { get; private set; }

    public Customer()
    {
        CustomerStatus = CustomerStatus.Default;
    }

    private void UpdateStatus()
    {
        if (TotalPurchases >= _totalPurchasesToBecomeVip)
        {
            CustomerStatus = CustomerStatus.VIP;
        }
    }
    
    public double applyDiscountForVip(double totalPrice) {
        return totalPrice * 0.98;
    }
    
}