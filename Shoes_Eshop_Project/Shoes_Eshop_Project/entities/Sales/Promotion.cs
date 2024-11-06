using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Shoes_Eshop_Project.Entities.Sales
{
    public class Promotion
    {
        public string Description { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public Promotion? MainPromotion { get; set; }
        public ICollection<Product> PromotionalProducts { get; set; }

        private static List<Promotion> _instances = new List<Promotion>();

        public Promotion(string description, DateTime startDate, DateTime endDate, ICollection<Product> promotionalProducts, Promotion? mainPromotion = null)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be null or empty.", nameof(description));
            if (startDate >= endDate)
                throw new ArgumentException("StartDate must be earlier than EndDate.");

            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            MainPromotion = mainPromotion;
            PromotionalProducts = promotionalProducts ?? new List<Product>();

            _instances.Add(this);
        }

        public bool IsPromotionActive()
        {
            DateTime today = DateTime.Now;
            return (today >= StartDate) && (today <= EndDate);
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
                _instances = JsonSerializer.Deserialize<List<Promotion>>(jsonData) ?? new List<Promotion>();
            }
        }

        public static List<Promotion> GetAll()
        {
            return new List<Promotion>(_instances);
        }
    }
}