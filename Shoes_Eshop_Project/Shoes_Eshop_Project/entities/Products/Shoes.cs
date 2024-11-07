using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using Shoes_Eshop_Project.entities;

namespace Shoes_Eshop_Project.Entities
{
    public class Shoes : Product
    {
        public LeatherType LeatherType { get; private set; }

        private static List<Shoes> _instances = new List<Shoes>();

        public Shoes(string name, string color, decimal price, LeatherType leatherType, int shoeSize, int amount) 
            : base(name, color, price)
        {
            LeatherType = leatherType;
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
                _instances = JsonSerializer.Deserialize<List<Shoes>>(jsonData) ?? new List<Shoes>();
            }
        }

        public static List<Shoes> GetAll()
        {
            return new List<Shoes>(_instances);
        }
        public static void ClearAll()
        {
            _instances.Clear();
        }
    }
}