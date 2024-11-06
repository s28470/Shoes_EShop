using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Shoes_Eshop_Project.entities
{
    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string? ApartmentNumber { get; set; }
        public string PostalCode { get; set; }

        private static List<Address> _addresses = new List<Address>();

        public Address(string city, string street, string houseNumber, string? apartmentNumber, string postalCode)
        {
            if (string.IsNullOrWhiteSpace(city) ||
                string.IsNullOrWhiteSpace(street) ||
                string.IsNullOrWhiteSpace(houseNumber) ||
                string.IsNullOrWhiteSpace(postalCode))
            {
                throw new ArgumentException("Argument is null or empty");
            }

            if (!ValidatePostalCode(postalCode))
            {
                throw new ArgumentException("index is not correct");
            }

            City = city;
            Street = street;
            HouseNumber = houseNumber;
            ApartmentNumber = apartmentNumber;
            PostalCode = postalCode;

            _addresses.Add(this);
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
            {
                throw new FileNotFoundException("File was not found", filePath);
            }

            string json = File.ReadAllText(filePath);
            var addresses = JsonSerializer.Deserialize<List<Address>>(json);
            if (addresses != null)
            {
                _addresses = addresses;
            }
        }

        public static List<Address> GetAll()
        {
            return new List<Address>(_addresses);
        }
    }
}