namespace Shoes_Eshop_Project.entities;
using System.ComponentModel.DataAnnotations;

public abstract class Customer
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string ContactNumber { get; set; }
    
    public string? Email { get; set; }

    [Required]
    public Address Address { get; set; }

    public decimal TotalPurchases { get; set; } 
    
    private static decimal _totalPurchasesToBecomeVip = 1000;

    private static double _vipDiscount = 0.02;
    
    public CustomerStatus CustomerStatus { get; private set; }

    protected Customer()
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
    
}