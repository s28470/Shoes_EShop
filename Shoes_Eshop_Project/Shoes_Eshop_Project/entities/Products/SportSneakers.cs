using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Shoes_Eshop_Project.Entities
{
    public class SportSneakers : Sneakers
    {
        public string SportType { get; private set; }

        private static List<SportSneakers> _instances = new List<SportSneakers>();

        public SportSneakers(string name, string color, decimal price, double weight, string cushioningTechnology, int shoeSize, int amount, string sportType)
            : base(name, color, price, weight, cushioningTechnology, shoeSize, amount)
        {
            if (string.IsNullOrWhiteSpace(sportType))
                throw new ArgumentException("SportType cannot be null or empty.", nameof(sportType));

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

        public static List<SportSneakers> GetAll()
        {
            return new List<SportSneakers>(_instances);
        }
    }
}