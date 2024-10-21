namespace Shoes_Eshop_Project.entities.Sales;

public class ShoppingCart
{
    public List<Product> Products{get; init;}
    
    private bool _isCompleted;

    private decimal _totalPrice;

    public ShoppingCart()
    {
        _isCompleted = false;
        Products = new List<Product>();
    }

    public decimal GetTotalPrice()
    {
        _totalPrice = Products.Select(product => product.Price).Sum();
        return _totalPrice;
    }
}