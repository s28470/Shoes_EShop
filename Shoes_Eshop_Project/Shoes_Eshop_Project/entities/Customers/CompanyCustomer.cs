using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Shoes_Eshop_Project.Entities
{
    public class CompanyCustomer
    {
        public string Occupation { get; private set; }
        public string WebSite { get; private set; }

        private static List<CompanyCustomer> _instances = new List<CompanyCustomer>();

        public CompanyCustomer(string occupation, string website)
        {
            if (string.IsNullOrWhiteSpace(occupation))
                throw new ArgumentException("Occupation cannot be null or empty.", nameof(occupation));
            if (string.IsNullOrWhiteSpace(website))
                throw new ArgumentException("Website cannot be null or empty.", nameof(website));

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

        public static List<CompanyCustomer> GetAll()
        {
            return new List<CompanyCustomer>(_instances);
        }
        public static void ClearAll()
        {
            _instances.Clear();
        }
    }
}