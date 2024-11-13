using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Shoes_Eshop_Project.Entities
{
    public class CasualSneakers
    {
        private string _styleType;
        private string _season;

        public string StyleType
        {
            get => _styleType;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("StyleType cannot be null or empty.", nameof(value));
                _styleType = value;
            }
        }

        public string Season
        {
            get => _season;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Season cannot be null or empty.", nameof(value));
                _season = value;
            }
        }

        private static List<CasualSneakers> _instances = new List<CasualSneakers>();

        public CasualSneakers(string styleType, string season)
        {
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

        public static List<CasualSneakers> GetAll() => new List<CasualSneakers>(_instances);

        public static void ClearAll() => _instances.Clear();
    }
}