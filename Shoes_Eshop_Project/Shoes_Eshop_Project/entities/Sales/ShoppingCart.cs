namespace Shoes_Eshop_Project.entities.Sales;

public class ShoppingCart
{
    private Dictionary<Product, int> Products{get; init;}
    
    private bool _isCompleted = false;

    private decimal _totalPrice = 0;

    public Customer Customer { get; set; }

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
    
    public void addProductToCart(Product product, int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        if (!Products.TryAdd(product, amount))
        {
            Products[product] += amount;
        }
    }

    public void RemoveProduct(Product product)
    {
        Products.Remove(product);

    }
    
    
}