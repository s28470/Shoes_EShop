namespace Shoes_Eshop_Project.entities;

public class Address
{
    public string City { get; set; }

    public string Street { get; set; }
    
    private string HouseNumber { get; set; }

    public string? ApartmentNumber { get; set; }
    
    public string PostalCode { get; set; }
    
}