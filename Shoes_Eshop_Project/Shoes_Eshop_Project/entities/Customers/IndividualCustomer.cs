using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Shoes_Eshop_Project.entities;
using Shoes_Eshop_Project.Entities;

namespace Shoes_Eshop_Project.Entities
{
    public class IndividualCustomer : Customer
    {
        private string _gender;
        private int? _age;

        public string Gender
        {
            get => _gender;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Gender cannot be null or empty.", nameof(value));
                _gender = value;
            }
        }

        public int? Age
        {
            get => _age;
            set
            {
                if (value.HasValue && (value < 0 || value > 120))
                    throw new ArgumentOutOfRangeException(nameof(value), "Age must be between 0 and 120.");
                _age = value;
            }
        }

        private static List<IndividualCustomer> _instances = new List<IndividualCustomer>();

        public IndividualCustomer(string name, string contactNumber, Address address, string gender, int? age = null, string? email = null)
            : base(name, contactNumber, address, email)
        {
            Gender = gender;
            Age = age;
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
                _instances = JsonSerializer.Deserialize<List<IndividualCustomer>>(jsonData) ?? new List<IndividualCustomer>();
            }
        }

        public static List<IndividualCustomer> GetAll() => new List<IndividualCustomer>(_instances);

        public static void ClearAll() => _instances.Clear();
    }
}