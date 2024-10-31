using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace Shoes_Eshop_Project.entities;

public class Address
{

    public string City { get; set; }


    public string Street { get; set; }
    

     public string HouseNumber { get; set; }

    public string? ApartmentNumber { get; set; }

    public string PostalCode { get; set; }
    
    public bool ValidatePostalCode(string postalCode)
    {
        return Regex.IsMatch(postalCode, @"^\d{5}$");
    }

    
}