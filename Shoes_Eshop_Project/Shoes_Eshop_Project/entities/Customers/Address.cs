using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using Shoes_Eshop_Project.Entities;

namespace Shoes_Eshop_Project.entities
{
    public class Address
    {
        private string _city;
        private string _street;
        private string _houseNumber;
        private string? _apartmentNumber;
        private string _postalCode;
        private Customer _customer;
        
        public string City
        {
            get => _city;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("City cannot be null or empty");
                _city = value;
            }
        }

        public string Street
        {
            get => _street;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Street cannot be null or empty");
                _street = value;
            }
        }

        public string HouseNumber
        {
            get => _houseNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("House number cannot be null or empty");
                _houseNumber = value;
            }
        }

        public string? ApartmentNumber
        {
            get => _apartmentNumber;
            set
            {
                if (!string.IsNullOrEmpty(value) && string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Apartment number cannot be only whitespace.");
                _apartmentNumber = value;
            }
        }

        public string PostalCode
        {
            get => _postalCode;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !ValidatePostalCode(value))
                    throw new ArgumentException("Postal code is invalid");
                _postalCode = value;
            }
        }

        private static List<Address> _addresses = new List<Address>();

        public Address(string city, string street, string houseNumber, string? apartmentNumber, string postalCode)
        {
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            ApartmentNumber = apartmentNumber;
            PostalCode = postalCode;

            _addresses.Add(this);
        }

        public void AddCustomer(Customer customer)
        {
            if (_customer == null && customer != null)
            {
                _customer = customer;
            }
        }
        
        
        private static bool ValidatePostalCode(string postalCode)
        {
            return Regex.IsMatch(postalCode, @"^\d{5}$");
        }

        public static void Save(string filePath)
        {
            var json = JsonSerializer.Serialize(_addresses);
            File.WriteAllText(filePath, json);
        }

        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File was not found", filePath);

            string json = File.ReadAllText(filePath);
            var addresses = JsonSerializer.Deserialize<List<Address>>(json);
            if (addresses != null)
                _addresses = addresses;
        }

        public static List<Address> GetAll() => new List<Address>(_addresses);

        public static void ClearAll() => _addresses.Clear();

        public Customer GetCustomer()
        {
            return _customer;
        }

        public bool HasCustomer()
        {
            return _customer != null;
        }

        public static void Remove(Address address)
        {
            
            if (_addresses.Contains(address))
            {
                _addresses.Remove(address);
            }
        }
        
        
        public void RemoveCustomer()
        {
            if (_customer != null)
            {
                var tempCustomer = _customer; 
                _customer = null;
                tempCustomer.RemoveAddress();
            }
        }
    }
}