using System.ComponentModel.DataAnnotations;

namespace Shoes_Eshop_Project.entities;

public class Sneakers : Product
{
    [Required]
    public double weight { get; set; }

    [Required]
    public string CushioningTechnology { get; set; }
    
}