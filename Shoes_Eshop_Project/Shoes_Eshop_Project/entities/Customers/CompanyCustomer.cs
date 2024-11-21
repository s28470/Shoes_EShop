using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Shoes_Eshop_Project.Entities
{
    public class CompanyCustomer
    {
        private string _occupation;
        private List<string> _websites = new List<string>() ;

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
        
        public List<string> Websites
        {
            get => new List<string>(_websites); 
            set
            {
                if (value == null || value.Any(string.IsNullOrWhiteSpace))
                    throw new ArgumentException();
                _websites = value;
            }
        }

   

        private static List<CompanyCustomer> _instances = new List<CompanyCustomer>();

        public CompanyCustomer(string occupation)
        {
            Occupation = occupation;
            _instances.Add(this);
        }

        public static void Save(string filePath)
        {
            var jsonData = JsonSerializer.Serialize(_instances);
            File.WriteAllText(filePath, jsonData);
        }
        
        public void AddWebsite(string website)
        {
            if (string.IsNullOrWhiteSpace(website))
                throw new ArgumentException();
    
            if (_websites.Contains(website))
                throw new InvalidOperationException("Website already exists in the list.");
    
            _websites.Add(website);
        }
        
        public void RemoveWebsite(string website)
        {
            if (string.IsNullOrWhiteSpace(website))
                throw new ArgumentException();

            if (!_websites.Contains(website))
                throw new InvalidOperationException();
    
            _websites.Remove(website);
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