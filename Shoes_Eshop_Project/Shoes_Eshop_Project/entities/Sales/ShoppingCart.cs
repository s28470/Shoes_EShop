namespace Shoes_Eshop_Project.entities.Sales;

public class ShoppingCart
{
    public Dictionary<Product, int> Products{get; init;}
    
    private bool _isCompleted;

    private decimal _totalPrice;

    public ShoppingCart()
    {
        _isCompleted = false;
        Products = new Dictionary<Product, int>();
    }

    public decimal GetTotalPrice()
    {
        _totalPrice = Products.Select(productAndAmount => productAndAmount.Key.Price * productAndAmount.Value).Sum();
        return _totalPrice;
    }
}