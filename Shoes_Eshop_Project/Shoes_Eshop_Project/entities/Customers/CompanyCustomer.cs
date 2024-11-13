using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Shoes_Eshop_Project.Entities
{
    public class CompanyCustomer
    {
        private string _occupation;
        private string _website;

        public string Occupation
        {
            get => _occupation;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Occupation cannot be null or empty.", nameof(value));
                _occupation = value;
            }
        }

        public string WebSite
        {
            get => _website;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Website cannot be null or empty.", nameof(value));
                _website = value;
            }
        }

        private static List<CompanyCustomer> _instances = new List<CompanyCustomer>();

        public CompanyCustomer(string occupation, string website)
        {
            Occupation = occupation;
            WebSite = website;
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
                _instances = JsonSerializer.Deserialize<List<CompanyCustomer>>(jsonData) ?? new List<CompanyCustomer>();
            }
        }

        public static List<CompanyCustomer> GetAll() => new List<CompanyCustomer>(_instances);

        public static void ClearAll() => _instances.Clear();
    }
}