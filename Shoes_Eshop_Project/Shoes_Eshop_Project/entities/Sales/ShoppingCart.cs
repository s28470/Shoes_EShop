using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Shoes_Eshop_Project.Entities;

namespace Shoes_Eshop_Project.Entities.Sales
{
    public class ShoppingCart
    {
        private Dictionary<Product, int> _products;
        private bool _isCompleted;
        private decimal _totalPrice;
        
        public Customer Customer { get; private set; }
        private static List<ShoppingCart> _instances = new List<ShoppingCart>();

        public ShoppingCart(Customer customer)
        {
            Customer = customer ?? throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
            _products = new Dictionary<Product, int>();
            _instances.Add(this);
        }

        public decimal GetTotalPrice()
        {
            _totalPrice = _products.Sum(item => item.Key.Price * item.Value);
            return _totalPrice;
        }
        
        public void AddProductToCart(Product product, int amount)
        {
            if (_isCompleted)
                throw new InvalidOperationException("Cannot modify a completed cart.");
            
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive.", nameof(amount));

            if (!_products.TryAdd(product, amount))
                _products[product] += amount;
        }

        public void RemoveProduct(Product product)
        {
            if (_isCompleted)
                throw new InvalidOperationException("Cannot modify a completed cart.");
            
            _products.Remove(product);
        }

        public void CompletePurchase()
        {
            if (_isCompleted)
                throw new InvalidOperationException("Cart has already been completed.");

            _isCompleted = true;
        }

        public static void Save(string filePath)
        {
            var jsonData = JsonSerializer.Serialize(_instances);
            File.WriteAllText(filePath, jsonData);
        }

        public static void Load(string filePath)
        {
            if (File.Exists(filePath))
            {
                var jsonData = File.ReadAllText(filePath);
                _instances = JsonSerializer.Deserialize<List<ShoppingCart>>(jsonData) ?? new List<ShoppingCart>();
            }
        }

        public static List<ShoppingCart> GetAll() => new List<ShoppingCart>(_instances);

        public static void ClearAll() => _instances.Clear();
    }
}