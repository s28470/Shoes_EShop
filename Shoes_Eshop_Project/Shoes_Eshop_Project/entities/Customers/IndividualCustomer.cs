using System.ComponentModel.DataAnnotations;

namespace Shoes_Eshop_Project.entities;

public class IndividualCustomer : Customer
{
    [Required]
    private string Gender { get; set; }
    
    [Range(0, 120)]
    private int? Age { get; set; }
    
} 