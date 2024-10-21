using System.ComponentModel.DataAnnotations;

namespace Shoes_Eshop_Project.entities;

public class Address
{
    [Required]
    public string City { get; set; }

    [Required]
    public string Street { get; set; }
    
    [Required]
    private string HouseNumber { get; set; }

    public string? ApartmentNumber { get; set; }
    [Required]
    public string PostalCode { get; set; }
    
}