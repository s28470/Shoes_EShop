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
            var sneakers = new Sneakers("Air Max", "Black", 150.00m, 1.2, "Air Cushion", 42, 10);

            Assert.Multiple(() =>
            {
                Assert.AreEqual("Air Max", sneakers.Name);
                Assert.AreEqual("Black", sneakers.Color);
                Assert.AreEqual(150.00m, sneakers.Price);
                Assert.AreEqual(1.2, sneakers.Weight);
                Assert.AreEqual("Air Cushion", sneakers.CushioningTechnology);
                Assert.AreEqual(42, sneakers.ShoeSize);
                Assert.AreEqual(10, sneakers.Amount);
            });
        }

        [Test]
        public void Constructor_NegativeWeight_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Sneakers("Air Max", "Black", 150.00m, -1.2, "Air Cushion", 42, 10));
        }

        [Test]
        public void Constructor_EmptyCushioningTechnology_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Sneakers("Air Max", "Black", 150.00m, 1.2, null, 42, 10));
        }

        [Test]
        public void Save_SneakersSavedToFile_FileShouldExist()
        {
            var sneakers = new Sneakers("Air Max", "Black", 150.00m, 1.2, "Air Cushion", 42, 10);

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
            var sneakers1 = new Sneakers("Air Max", "Black", 150.00m, 1.2, "Air Cushion", 42, 10);
            var sneakers2 = new Sneakers("Zoom", "White", 120.00m, 0.9, "Zoom Air", 41, 15);

            Sneakers.Save(_filePath);
            Sneakers.Load(_filePath);

            var loadedSneakers = Sneakers.GetAll();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(2, loadedSneakers.Count);
                Assert.AreEqual("Air Max", loadedSneakers[0].Name);
                Assert.AreEqual("Zoom", loadedSneakers[1].Name);
                Assert.AreEqual(1.2, loadedSneakers[0].Weight);
                Assert.AreEqual(0.9, loadedSneakers[1].Weight);
            });
        }

        [Test]
        public void ClearAll_ShouldRemoveAllSneakers()
        {
            var sneakers = new Sneakers("Air Max", "Black", 150.00m, 1.2, "Air Cushion", 42, 10);

            Sneakers.ClearAll();

            Assert.AreEqual(0, Sneakers.GetAll().Count);
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