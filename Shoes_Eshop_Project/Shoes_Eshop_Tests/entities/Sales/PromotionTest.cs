using NUnit.Framework;
using Shoes_Eshop_Project.Entities.Sales;
using Shoes_Eshop_Project.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace Shoes_Eshop_Project.Tests.Sales
{
    [TestFixture]
    public class PromotionTests
    {
        private string _filePath;

        [SetUp]
        public void Setup()
        {
            _filePath = Path.Combine(Path.GetTempPath(), "promotions.json");
            Promotion.ClearAll();
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [Test]
        public void Constructor_ValidParameters_ShouldCreatePromotion()
        {
            var products = new List<Product> { new Sneakers("Air Max", "Black", 100.00m, 1.2, "Air Cushion", 42, 10) };
            var promotion = new Promotion("Summer Sale", DateTime.Now.AddDays(-1), DateTime.Now.AddDays(10), products);

            Assert.Multiple(() =>
            {
                Assert.AreEqual("Summer Sale", promotion.Description);
                Assert.AreEqual(1, promotion.PromotionalProducts.Count);
                Assert.IsTrue(promotion.StartDate < promotion.EndDate);
            });
        }

        [Test]
        public void Constructor_InvalidDescription_ShouldThrowArgumentException()
        {
            var products = new List<Product> { new Sneakers("Air Max", "Black", 100.00m, 1.2, "Air Cushion", 42, 10) };
            Assert.Throws<ArgumentException>(() =>
                new Promotion("", DateTime.Now, DateTime.Now.AddDays(1), products));
        }

        [Test]
        public void Constructor_InvalidDates_ShouldThrowArgumentException()
        {
            var products = new List<Product> { new Sneakers("Air Max", "Black", 100.00m, 1.2, "Air Cushion", 42, 10) };
            Assert.Throws<ArgumentException>(() =>
                new Promotion("Invalid Promotion", DateTime.Now.AddDays(1), DateTime.Now, products));
        }

        [Test]
        public void IsPromotionActive_WithinDateRange_ShouldReturnTrue()
        {
            var products = new List<Product> { new Sneakers("Air Max", "Black", 100.00m, 1.2, "Air Cushion", 42, 10) };
            var promotion = new Promotion("Active Sale", DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), products);
            Assert.IsTrue(promotion.IsPromotionActive());
        }

        [Test]
        public void IsPromotionActive_OutsideDateRange_ShouldReturnFalse()
        {
            var products = new List<Product> { new Sneakers("Air Max", "Black", 100.00m, 1.2, "Air Cushion", 42, 10) };
            var promotion = new Promotion("Inactive Sale", DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-1), products);
            Assert.IsFalse(promotion.IsPromotionActive());
        }

        [Test]
        public void Save_PromotionsSavedToFile_FileShouldExist()
        {
            var products = new List<Product> { new Sneakers("Air Max", "Black", 100.00m, 1.2, "Air Cushion", 42, 10) };
            var promotion = new Promotion("Test Save", DateTime.Now, DateTime.Now.AddDays(5), products);
            Promotion.Save(_filePath);

            Assert.IsTrue(File.Exists(_filePath));
        }

        [Test]
        public void Load_ValidFile_ShouldLoadPromotions()
        {
            var products1 = new List<Product> { new Sneakers("Air Max", "Red", 80.00m, 1.2, "Air Cushion", 42, 10) };
            var products2 = new List<Product> { new Sneakers("Boots", "Brown", 120.00m, 1.5, "Gel Cushion", 44, 15) };

            var promotion1 = new Promotion("Spring Sale", DateTime.Now.AddDays(-5), DateTime.Now.AddDays(5), products1);
            var promotion2 = new Promotion("Winter Sale", DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10), products2);

            Promotion.Save(_filePath);
            Promotion.Load(_filePath);

            var loadedPromotions = Promotion.GetAll();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(2, loadedPromotions.Count);
                Assert.AreEqual("Spring Sale", loadedPromotions[0].Description);
                Assert.AreEqual("Winter Sale", loadedPromotions[1].Description);
                Assert.AreEqual(1, loadedPromotions[0].PromotionalProducts.Count);
                Assert.AreEqual(1, loadedPromotions[1].PromotionalProducts.Count);

                var product1 = loadedPromotions[0].PromotionalProducts as List<Product>;
                var product2 = loadedPromotions[1].PromotionalProducts as List<Product>;

                Assert.AreEqual("Air Max", product1?[0].Name);
                Assert.AreEqual("Boots", product2?[0].Name);
            });
        }

        [Test]
        public void ClearAll_ShouldRemoveAllPromotions()
        {
            var products = new List<Product> { new Sneakers("Air Max", "Black", 100.00m, 1.2, "Air Cushion", 42, 10) };
            var promotion = new Promotion("Test Clear", DateTime.Now, DateTime.Now.AddDays(1), products);
            Promotion.ClearAll();
            Assert.AreEqual(0, Promotion.GetAll().Count);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            Promotion.ClearAll();
        }
    }
}