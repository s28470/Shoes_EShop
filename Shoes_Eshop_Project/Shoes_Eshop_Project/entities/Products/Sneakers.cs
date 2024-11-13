using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Shoes_Eshop_Project.Entities
{
    public class Sneakers : Product
    {
        private double _weight;
        private string _cushioningTechnology;

        public double Weight
        {
            get => _weight;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Weight must be positive.", nameof(value));
                _weight = value;
            }
        }

        public string CushioningTechnology
        {
            get => _cushioningTechnology;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("CushioningTechnology cannot be null or empty.", nameof(value));
                _cushioningTechnology = value;
            }
        }

        private static List<Sneakers> _instances = new List<Sneakers>();

        public Sneakers(string name, string color, decimal price, double weight, string cushioningTechnology, int shoeSize, int amount)
            : base(name, color, price)
        {
            Weight = weight;
            CushioningTechnology = cushioningTechnology;
            ShoeSize = shoeSize;
            Amount = amount;
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
                _instances = JsonSerializer.Deserialize<List<Sneakers>>(jsonData) ?? new List<Sneakers>();
            }
        }

        public static List<Sneakers> GetAll() => new List<Sneakers>(_instances);

        public static void ClearAll() => _instances.Clear();
    }
}