using System.ComponentModel.DataAnnotations;

namespace Shoes_Eshop_Project.entities;

public class CompanyCustomer
{
    [Required]
    public string Ocupation { get; set; }
    
    [Required]
    public string WebSite { get; set; }
}