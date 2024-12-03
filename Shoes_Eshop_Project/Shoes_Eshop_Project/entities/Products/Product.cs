using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Shoes_Eshop_Project.Entities.Sales;

namespace Shoes_Eshop_Project.Entities
{
    public class Product
    {
        private string _name;
        private decimal _price;
        private string _color;

        private IList<Promotion> _related_promotions = new List<Promotion>();

        public string Name
        {
            get => _name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be null or empty.", nameof(value));
                _name = value;
            }
        }

        [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
        public string Description { get; set; }

        public decimal Price
        {
            get => _price;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Price cannot be negative.");
                _price = value;
            }
        }

        public int ShoeSize { get; set; }

        public string Color
        {
            get => _color;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Color cannot be null or empty.", nameof(value));
                _color = value;
            }
        }

        public int Amount { get; set; }

        public Product(string name, string color, decimal price)
        {
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

        public void AddPromotion(Promotion promotion)
        {
            if (promotion == null)
            {
                throw new ArgumentException();
            }

            if (!_related_promotions.Contains(promotion))
            {
                _related_promotions.Add(promotion);
                promotion.AddProduct(this);
            }
        }

        public void RemovePromotion(Promotion promotion)
        {
            if (promotion == null)
            {
                throw new ArgumentNullException();
            }

            if (_related_promotions.Contains(promotion))
            {
                _related_promotions.Remove(promotion);
                promotion.RemoveProduct(this);
            }
            
            
        }

        public void UpdatePromotion(Promotion oldPromotion, Promotion newPromotion)
        {
            oldPromotion.RemoveProduct(this);
            newPromotion.AddProduct(this);
        }
        
        public IList<Promotion> GetPromotions()
        {
            return new Collection<Promotion>(_related_promotions);
        }
        
        public bool HasPromotion(Promotion promotion)
        {
            return _related_promotions.Contains(promotion);
        }
        
    }
}