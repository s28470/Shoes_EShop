using NUnit.Framework;
using Shoes_Eshop_Project.Entities;
using Shoes_Eshop_Project.Entities.Sales;
using System;
using System.IO;
using System.Collections.Generic;

namespace Shoes_Eshop_Project.Tests
{
    [TestFixture]
    public class PromotionTests
    {
        private string _filePath;
        private List<Slippers> _promotionalSlippers;

        [SetUp]
        public void Setup()
        {
            _filePath = Path.Combine(Path.GetTempPath(), "promotions.json");
            _promotionalSlippers = new List<Slippers>
            {
                new Slippers("Winter Slippers", "Grey", 49.99m, "Non-slip", 42, 10),
                new Slippers("Summer Slippers", "Blue", 39.99m, "Anti-slip", 40, 5)
            };
            Promotion.ClearAll();
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }
        

        [Test]
        public void Constructor_EmptyDescription_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Promotion("", DateTime.Now, DateTime.Now.AddDays(1), null));
        }

        [Test]
        public void Constructor_EndDateBeforeStartDate_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Promotion("Invalid Promotion", DateTime.Now.AddDays(5), DateTime.Now.AddDays(1), null));
        }

        [Test]
        public void IsPromotionActive_WhenActive_ShouldReturnTrue()
        {
            var startDate = DateTime.Now.AddDays(-1);
            var endDate = DateTime.Now.AddDays(5);
            var promotion = new Promotion("Active Sale", startDate, endDate, null);
            Assert.IsTrue(promotion.IsPromotionActive());
        }

        [Test]
        public void IsPromotionActive_WhenInactive_ShouldReturnFalse()
        {
            var startDate = DateTime.Now.AddDays(-10);
            var endDate = DateTime.Now.AddDays(-5);
            var promotion = new Promotion("Expired Sale", startDate, endDate, null);
            Assert.IsFalse(promotion.IsPromotionActive());
        }

        [Test]
        public void Save_PromotionsSavedToFile_FileShouldExist()
        {
            var promotion = new Promotion("Winter Sale", DateTime.Now, DateTime.Now.AddDays(5), null);
            Promotion.Save(_filePath);
            Assert.IsTrue(File.Exists(_filePath));
        }

        [Test]
        public void Load_FileDoesNotExist_ShouldNotThrowException()
        {
            Assert.DoesNotThrow(() => Promotion.Load("nonexistent.json"));
        }

        [Test]
        public void GetAll_ShouldReturnListOfPromotions()
        {
            var promotion1 = new Promotion("Winter Sale", DateTime.Now, DateTime.Now.AddDays(5), null);
            var promotion2 = new Promotion("Summer Sale", DateTime.Now.AddDays(10), DateTime.Now.AddDays(15), null);
            var promotions = Promotion.GetAll();
            Assert.AreEqual(2, promotions.Count);
        }

        [Test]
        public void ClearAll_ShouldRemoveAllPromotions()
        {
            var promotion1 = new Promotion("Winter Sale", DateTime.Now, DateTime.Now.AddDays(5), null);
            var promotion2 = new Promotion("Summer Sale", DateTime.Now.AddDays(10), DateTime.Now.AddDays(15), null);
            Promotion.ClearAll();
            var promotions = Promotion.GetAll();
            Assert.AreEqual(0, promotions.Count);
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
