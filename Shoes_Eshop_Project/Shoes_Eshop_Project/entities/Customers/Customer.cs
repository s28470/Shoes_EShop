namespace Shoes_Eshop_Project.entities;

public abstract class Customer
{
    public string Name { get; set; }
    
    public string ContactNumber { get; set; }

    public string? Email { get; set; }

    public Address Address { get; set; }

    public decimal TotalPurchases { get; set; } 
    
    private static decimal _totalPurchasesToBecomeVip = 1000;
    
    public CustomerStatus CustomerStatus { get; private set; }

    private void UpdateStatus()
    {
        // if TotalPurchases > _totalPurchasesToBecomeVip then make VIP
    }
    
}