using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Shoes_Eshop_Project.Entities
{
    public class SportSneakers : Sneakers
    {
        private string _sportType;

        public string SportType
        {
            get => _sportType;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("SportType cannot be null or empty.", nameof(value));
                _sportType = value;
            }
        }

        private static List<SportSneakers> _instances = new List<SportSneakers>();

        public SportSneakers(string name, string color, decimal price, double weight, string cushioningTechnology, int shoeSize, int amount, string sportType)
            : base(name, color, price, weight, cushioningTechnology, shoeSize, amount)
        {
            SportType = sportType;
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
                _instances = JsonSerializer.Deserialize<List<SportSneakers>>(jsonData) ?? new List<SportSneakers>();
            }
        }

        public static List<SportSneakers> GetAll() => new List<SportSneakers>(_instances);

        public static void ClearAll() => _instances.Clear();
    }
}