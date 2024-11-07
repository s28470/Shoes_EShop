using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using Shoes_Eshop_Project.entities;

namespace Shoes_Eshop_Project.Entities
{
    public class Customer
    {
        public string Name { get; private set; }
        public string ContactNumber { get; private set; }
        public string? Email { get; private set; }
        public Address Address { get; private set; }
        public decimal TotalPurchases { get; set; }
        private static decimal _totalPurchasesToBecomeVip = 1000;
        private static double _vipDiscount = 0.02;
        public CustomerStatus CustomerStatus { get; private set; }

        private static List<Customer> _instances = new List<Customer>();

        public Customer(string name, string contactNumber, Address address, string? email = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(contactNumber))
                throw new ArgumentException("ContactNumber cannot be null or empty.", nameof(contactNumber));
            if (address == null)
                throw new ArgumentNullException(nameof(address), "Address cannot be null.");

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

        public static List<Customer> GetAll()
        {
            return new List<Customer>(_instances);
        }

        private void UpdateStatus()
        {
            if (TotalPurchases >= _totalPurchasesToBecomeVip)
            {
                CustomerStatus = CustomerStatus.VIP;
            }
        }

        public double ApplyDiscountForVip(double totalPrice)
        {
            return CustomerStatus == CustomerStatus.VIP ? totalPrice * (1 - _vipDiscount) : totalPrice;
        }
        public static void ClearAll()
        {
            _instances.Clear();
        }
    }
}