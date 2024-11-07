using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Shoes_Eshop_Project.Entities
{
    public class CasualSneakers
    {
        private string StyleType { get; set; }
        private string Season { get; set; }

        private static List<CasualSneakers> _instances = new List<CasualSneakers>();

        public CasualSneakers(string styleType, string season)
        {
            if (string.IsNullOrWhiteSpace(styleType))
                throw new ArgumentException("StyleType cannot be null or empty.", nameof(styleType));
            if (string.IsNullOrWhiteSpace(season))
                throw new ArgumentException("Season cannot be null or empty.", nameof(season));

            StyleType = styleType;
            Season = season;
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
                _instances = JsonSerializer.Deserialize<List<CasualSneakers>>(jsonData) ?? new List<CasualSneakers>();
            }
        }

        public static List<CasualSneakers> GetAll()
        {
            return new List<CasualSneakers>(_instances);
        }
        public static void ClearAll()
        {
            _instances.Clear();
        }
    }
}