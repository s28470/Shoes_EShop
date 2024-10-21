using System.ComponentModel.DataAnnotations;

namespace Shoes_Eshop_Project.entities;

public abstract class Product
{
    [Required]
    public string Name { get; set; }

    // 200 symbols
    [Length(0,200)]
    public string description { get; set; }
    
    [Required]
    public decimal Price { get; set; }

    [Required]
    public int ShoeSize { get; set; }

    [Required]
    public string Color { get; set; }
    
    [Required]
    public int Amount { get; set; }
    
}