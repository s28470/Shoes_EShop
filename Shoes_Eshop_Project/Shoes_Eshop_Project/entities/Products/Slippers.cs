using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Shoes_Eshop_Project.Entities
{
    public class Slippers : Product
    {
        public string Grip { get; private set; }

        private static List<Slippers> _instances = new List<Slippers>();

        public Slippers(string name, string color, decimal price, string grip, int shoeSize, int amount)
            : base(name, color, price)
        {
            if (string.IsNullOrWhiteSpace(grip))
                throw new ArgumentException("Grip cannot be null or empty.", nameof(grip));

            Grip = grip;
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
                _instances = JsonSerializer.Deserialize<List<Slippers>>(jsonData) ?? new List<Slippers>();
            }
        }

        public static List<Slippers> GetAll()
        {
            return new List<Slippers>(_instances);
        }

        public static void ClearAll()
        {
            _instances.Clear();
        }
    }
}