using System.ComponentModel.DataAnnotations;

namespace Shoes_Eshop_Project.entities;

public class Shoes : Product
{
    [Required]
    public LeatherType LeatherType { get; set; }
}