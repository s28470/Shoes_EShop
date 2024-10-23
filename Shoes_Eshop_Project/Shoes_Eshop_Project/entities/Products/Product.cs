using System.ComponentModel.DataAnnotations;

namespace Shoes_Eshop_Project.entities
{
    public abstract class Product
    {
        [Required]
        public string Name { get; set; }
        
        [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int ShoeSize { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public int Amount { get; set; }
        
        public void UpdateStock(int quantitySold)
        {
            if (quantitySold < 0)
            {
                throw new ArgumentException("Quantity sold cannot be negative.");
            }
            if (quantitySold > Amount)
            {
                throw new InvalidOperationException("Insufficient stock.");
            }
            Amount -= quantitySold;
        }
    }
}