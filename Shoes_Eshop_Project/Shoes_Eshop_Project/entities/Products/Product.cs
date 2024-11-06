using System;
using System.ComponentModel.DataAnnotations;

namespace Shoes_Eshop_Project.Entities
{
    public abstract class Product
    {
        public string Name { get; private set; }
        
        [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
        public string Description { get; set; }

        public decimal Price { get; private set; }
        public int ShoeSize { get; set; }
        public string Color { get; private set; }
        public int Amount { get; set; }

        protected Product(string name, string color, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(color))
                throw new ArgumentException("Color cannot be null or empty.", nameof(color));
            if (price < 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");

            Name = name;
            Color = color;
            Price = price;
        }
        
        public void UpdateStock(int quantitySold)
        {
            if (quantitySold < 0)
                throw new ArgumentException("Quantity sold cannot be negative.");
            if (quantitySold > Amount)
                throw new InvalidOperationException("Insufficient stock.");
            Amount -= quantitySold;
        }
    }
}