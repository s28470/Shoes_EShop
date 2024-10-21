namespace Shoes_Eshop_Project.entities;

public abstract class Product
{
    public string Name { get; set; }

    // 200 symbols
    public string description { get; set; }

    public decimal Price { get; set; }

    public int ShoeSize { get; set; }

    public string Color { get; set; }
    
    public int Amount { get; set; }
    
}