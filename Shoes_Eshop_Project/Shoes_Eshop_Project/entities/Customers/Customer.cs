using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using Shoes_Eshop_Project.entities;
using Shoes_Eshop_Project.Entities.Sales;
using Shoes_Eshop_Project.extensions;

namespace Shoes_Eshop_Project.Entities
{
    public class Customer
    {
        private string _name;
        private string _contactNumber;
        private string? _email;
        private Address _address;
        private List<ShoppingCart> _relatedShoppingCarts = new List<ShoppingCart>();

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be null or empty.", nameof(value));
                _name = value;
            }
        }

        public string ContactNumber
        {
            get => _contactNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ContactNumber cannot be null or empty.", nameof(value));
                _contactNumber = value;
            }
        }

        public string? Email
        {
            get => _email;
            set
            {
                if (value != null && string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Email cannot be empty or whitespace.");
                }
                _email = value;
            }
        }

        public Address Address
        {
            get => _address;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "Address cannot be null.");

                if (value.HasCustomer())
                    throw new ArgumentException("The address is already associated with another customer.");

                if (_address != null)
                    Address.Remove(_address);

                _address = value;
                _address.AddCustomer(this);
            }
        }

        public List<ShoppingCart> GetShoppingCarts()
        {
            return new List<ShoppingCart>(_relatedShoppingCarts);
        }

        public IReadOnlyList<ShoppingCart> ShoppingCarts => _relatedShoppingCarts.AsReadOnly();

        public void AddShoppingCart(ShoppingCart cart)
        {
            if (cart == null)
                throw new ArgumentNullException(nameof(cart), "Cart cannot be null.");

            if (cart.Customer != this)
                throw new InvalidOperationException("This cart belongs to a different customer.");

            if (!_relatedShoppingCarts.Contains(cart))
                _relatedShoppingCarts.Add(cart);
        }

        public void RemoveShoppingCart(ShoppingCart cart)
        {
            if (_relatedShoppingCarts.Contains(cart))
            {
                _relatedShoppingCarts.Remove(cart);
                cart.UnsetCustomer();
            }
        }

        public decimal TotalPurchases
        {
            get => _totalPurchases;
            set
            {
                _totalPurchases = value;
                UpdateStatus();
            }
        }

        private decimal _totalPurchases;
        private static decimal _totalPurchasesToBecomeVip = 1000;
        private static double _vipDiscount = 0.02;
        public CustomerStatus CustomerStatus { get; private set; }

        private static List<Customer> _instances = new List<Customer>();

        public Customer(string name, string contactNumber, Address address, string? email = null)
        {
            Name = name;
            ContactNumber = contactNumber;
            Address = address;
            Email = email;
            CustomerStatus = CustomerStatus.Default;
            _instances.Add(this);
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
                _instances = JsonSerializer.Deserialize<List<Customer>>(jsonData) ?? new List<Customer>();
            }
        }

        public static List<Customer> GetAll() => new List<Customer>(_instances);

        private void UpdateStatus()
        {
            if (TotalPurchases >= _totalPurchasesToBecomeVip)
            {
                CustomerStatus = CustomerStatus.VIP;
            }
        }

        public void RemoveAddress()
        {
            if (_address != null)
            {
                var tempAddress = _address;
                _address = null;
                tempAddress.RemoveCustomer();
            }
        }

        public double ApplyDiscountForVip(double totalPrice)
        {
            return CustomerStatus == CustomerStatus.VIP ? totalPrice * (1 - _vipDiscount) : totalPrice;
        }

        public static void Remove(Customer customer)
        {
            if (_instances.Contains(customer))
            {
                _instances.Remove(customer);
                foreach (var relatedShoppingCart in customer._relatedShoppingCarts)
                {
                    ShoppingCart.Remove(relatedShoppingCart);
                }
                Address.Remove(customer._address);
            }
        }

        public static void ClearAll() => _instances.Clear();
    }
}