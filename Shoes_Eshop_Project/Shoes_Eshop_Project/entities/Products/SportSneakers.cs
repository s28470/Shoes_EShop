using System.ComponentModel.DataAnnotations;

namespace Shoes_Eshop_Project.entities;

public class SportSneakers : Sneakers
{
    [Required]
    public string SportType { get; set; }
}