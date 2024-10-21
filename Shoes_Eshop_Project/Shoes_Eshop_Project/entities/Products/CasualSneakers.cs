using System.ComponentModel.DataAnnotations;

namespace Shoes_Eshop_Project.entities;

public class CasualSneakers
{
    [Required]
    private string StyleType { get; set; }
    
    [Required]
    private string Season { get; set; }
    
}