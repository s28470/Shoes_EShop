using NUnit.Framework;
using Shoes_Eshop_Project.Entities;
using System;
using System.IO;
using System.Collections.Generic;

namespace Shoes_Eshop_Project.Tests
{
    [TestFixture]
    public class SneakersTests
    {
        private string _filePath;

        [SetUp]
        public void Setup()
        {
            _filePath = Path.Combine(Path.GetTempPath(), "sneakers.json");
            Sneakers.ClearAll();
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [Test]
        public void Constructor_ValidParameters_ShouldCreateSneakers()
        {
            var sneakers = new Sneakers("Running Shoes", "Blue", 120.00m, 0.75, "Air Cushion", 42, 15);
            Assert.AreEqual("Running Shoes", sneakers.Name);
            Assert.AreEqual("Blue", sneakers.Color);
            Assert.AreEqual(120.00m, sneakers.Price);
            Assert.AreEqual(0.75, sneakers.Weight);
            Assert.AreEqual("Air Cushion", sneakers.CushioningTechnology);
            Assert.AreEqual(42, sneakers.ShoeSize);
            Assert.AreEqual(15, sneakers.Amount);
        }

        [Test]
        public void Constructor_NonPositiveWeight_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Sneakers("Running Shoes", "Blue", 120.00m, -0.5, "Air Cushion", 42, 15));
        }

        [Test]
        public void Constructor_EmptyCushioningTechnology_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Sneakers("Running Shoes", "Blue", 120.00m, 0.75, "", 42, 15));
        }

        [Test]
        public void Save_SneakersSavedToFile_FileShouldExist()
        {
            var sneakers = new Sneakers("Running Shoes", "Blue", 120.00m, 0.75, "Air Cushion", 42, 15);
            Sneakers.Save(_filePath);
            Assert.IsTrue(File.Exists(_filePath));
        }

        [Test]
        public void Load_FileDoesNotExist_ShouldNotThrowException()
        {
            Assert.DoesNotThrow(() => Sneakers.Load("nonexistent.json"));
        }

        [Test]
        public void Load_ValidFile_ShouldLoadSneakers()
        {
            var sneakers1 = new Sneakers("Running Shoes", "Blue", 120.00m, 0.75, "Air Cushion", 42, 15);
            var sneakers2 = new Sneakers("Walking Shoes", "Black", 100.00m, 0.85, "Foam Cushion", 41, 10);
            Sneakers.Save(_filePath);

            Sneakers.ClearAll();
            Sneakers.Load(_filePath);
            var loadedSneakers = Sneakers.GetAll();

            Assert.AreEqual(2, loadedSneakers.Count);
            Assert.AreEqual("Running Shoes", loadedSneakers[0].Name);
            Assert.AreEqual("Walking Shoes", loadedSneakers[1].Name);
        }

        [Test]
        public void GetAll_ShouldReturnListOfSneakers()
        {
            var sneakers1 = new Sneakers("Running Shoes", "Blue", 120.00m, 0.75, "Air Cushion", 42, 15);
            var sneakers2 = new Sneakers("Walking Shoes", "Black", 100.00m, 0.85, "Foam Cushion", 41, 10);
            var sneakersList = Sneakers.GetAll();
            Assert.AreEqual(2, sneakersList.Count);
        }

        [Test]
        public void ClearAll_ShouldRemoveAllSneakers()
        {
            var sneakers1 = new Sneakers("Running Shoes", "Blue", 120.00m, 0.75, "Air Cushion", 42, 15);
            var sneakers2 = new Sneakers("Walking Shoes", "Black", 100.00m, 0.85, "Foam Cushion", 41, 10);
            Sneakers.ClearAll();
            var sneakersList = Sneakers.GetAll();
            Assert.AreEqual(0, sneakersList.Count);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            Sneakers.ClearAll();
        }
    }
}
