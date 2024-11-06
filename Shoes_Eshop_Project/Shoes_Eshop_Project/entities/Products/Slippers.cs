using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Shoes_Eshop_Project.Entities
{
    public class Slippers
    {
        public string Grip { get; private set; }

        private static List<Slippers> _instances = new List<Slippers>();

        public Slippers(string grip)
        {
            if (string.IsNullOrWhiteSpace(grip))
                throw new ArgumentException("Grip cannot be null or empty.", nameof(grip));

            Grip = grip;
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
    }
}