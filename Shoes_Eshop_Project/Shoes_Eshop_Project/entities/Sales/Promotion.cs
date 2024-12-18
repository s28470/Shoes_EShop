using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Shoes_Eshop_Project.Entities.Sales
{
    public class Promotion
    {   
        private string _description;
        private DateTime _startDate;
        private DateTime _endDate;
        
        

        public string Description
        {
            get => _description;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Description cannot be null or empty.", nameof(value));
                _description = value;
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            private set => _startDate = value;
        }

        public DateTime EndDate
        {
            get => _endDate;
            private set => _endDate = value;
        }

        private Promotion? _mainPromotion;

        public Promotion? MainPromotion
        {
            get => _mainPromotion;
            set
            {
                if (_mainPromotion == value) 
                    return; 

                if (_mainPromotion != null)
                {
                    RemoveMainPromotion();
                }

                if (value == null)
                {
                    return;
                }

                _mainPromotion = value;

                if (!_mainPromotion._relatedPromotions.Contains(this))
                {
                    _mainPromotion.AddRelatedPromotion(this);
                }
            }
        }
        public ICollection<Product> PromotionalProducts { get; set; }

        private static List<Promotion> _instances = new List<Promotion>();

        private List<Promotion> _relatedPromotions = new List<Promotion>();

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
            PromotionalProducts = promotionalProducts;
            foreach (var promotionalProduct in promotionalProducts)
            {
                promotionalProduct.AddPromotion(this);
            }

            _instances.Add(this);
        }

        public bool IsPromotionActive()
        {
            DateTime today = DateTime.Now;
            return today >= StartDate && today <= EndDate;
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
        
        public void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentException();
            }

            if (!PromotionalProducts.Contains(product))
            {
                PromotionalProducts.Add(product);
                product.AddPromotion(this);
            }
        }

        public void RemoveProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException();
            }

            if (PromotionalProducts.Contains(product))
            {
                PromotionalProducts.Remove(product);
                product.RemovePromotion(this);
            }
            
        }
        public void AddMainPromotion(Promotion mainPromotion)
        {
            if (MainPromotion != mainPromotion)
            {
                MainPromotion = mainPromotion;
                if (mainPromotion != null && !mainPromotion._relatedPromotions.Contains(this))
                {
                    mainPromotion.AddRelatedPromotion(this);
                }
            }
        }

        public void RemoveMainPromotion()
        {
            if (_mainPromotion != null)
            {
                var oldMainPromotion = _mainPromotion;
                _mainPromotion = null; 
                oldMainPromotion._relatedPromotions.Remove(this);
            }
        }

        public void AddRelatedPromotion(Promotion relatedPromotion)
        {
            if (relatedPromotion == null || _relatedPromotions.Contains(relatedPromotion)) return;

            _relatedPromotions.Add(relatedPromotion);
            if (relatedPromotion.MainPromotion != this)
            {
                relatedPromotion.AddMainPromotion(this);
            }
        }

        public void RemoveRelatedPromotion(Promotion relatedPromotion)
        {
            if (relatedPromotion == null || !_relatedPromotions.Contains(relatedPromotion)) return;

            _relatedPromotions.Remove(relatedPromotion);
            if (relatedPromotion.MainPromotion == this)
            {
                relatedPromotion.RemoveMainPromotion();
            }
        }

        public void ChangeMainPromotion(Promotion newMainPromotion)
        {
            RemoveMainPromotion();
            AddMainPromotion(newMainPromotion);
        }

        public List<Promotion> GetRelatedPromotions()
        {
            return new List<Promotion>(_relatedPromotions);
        }


        public bool HasProduct(Product product)
        {
            return PromotionalProducts.Contains(product);
        }
        
        public ICollection<Product> GetProducts()
        {
            return new List<Product>(PromotionalProducts);
        }

        public static List<Promotion> GetAll() => new List<Promotion>(_instances);

        public static void ClearAll() => _instances.Clear();
    }
}