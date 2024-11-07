using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Shoes_Eshop_Project.Entities
{
    public class Sneakers : Product
    {
        public double Weight { get; private set; }
        public string CushioningTechnology { get; private set; }

        private static List<Sneakers> _instances = new List<Sneakers>();

        public Sneakers(string name, string color, decimal price, double weight, string cushioningTechnology, int shoeSize, int amount)
            : base(name, color, price)
        {
            if (weight <= 0)
                throw new ArgumentException("Weight must be positive.", nameof(weight));
            if (string.IsNullOrWhiteSpace(cushioningTechnology))
                throw new ArgumentException("CushioningTechnology cannot be null or empty.", nameof(cushioningTechnology));

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

        public static List<Sneakers> GetAll()
        {
            return new List<Sneakers>(_instances);
        }
        public static void ClearAll()
        {
            _instances.Clear();
        }
    }
}