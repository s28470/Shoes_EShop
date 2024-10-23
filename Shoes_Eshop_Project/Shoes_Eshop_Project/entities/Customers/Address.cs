using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace Shoes_Eshop_Project.entities;

public class Address
{
    [Required]
    public string City { get; set; }

    [Required]
    public string Street { get; set; }
    
    [Required]
     public string HouseNumber { get; set; }

    public string? ApartmentNumber { get; set; }
    [Required]
    public string PostalCode { get; set; }
    
    public bool ValidatePostalCode(string postalCode)
    {
        return Regex.IsMatch(postalCode, @"^\d{5}$");
    }

    
}