using NUnit.Framework;
using Shoes_Eshop_Project.Entities;
using System;
using System.IO;
using System.Collections.Generic;

namespace Shoes_Eshop_Project.Tests
{
    [TestFixture]
    public class SportSneakersTests
    {
        private string _filePath;

        [SetUp]
        public void Setup()
        {
            _filePath = Path.Combine(Path.GetTempPath(), "sportSneakers.json");
            SportSneakers.ClearAll();
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [Test]
        public void Constructor_ValidParameters_ShouldCreateSportSneakers()
        {
            var sportSneakers = new SportSneakers("Air Zoom", "Blue", 200.00m, 1.1, "Zoom Air", 43, 20, "Running");

            Assert.Multiple(() =>
            {
                Assert.AreEqual("Air Zoom", sportSneakers.Name);
                Assert.AreEqual("Blue", sportSneakers.Color);
                Assert.AreEqual(200.00m, sportSneakers.Price);
                Assert.AreEqual(1.1, sportSneakers.Weight);
                Assert.AreEqual("Zoom Air", sportSneakers.CushioningTechnology);
                Assert.AreEqual(43, sportSneakers.ShoeSize);
                Assert.AreEqual(20, sportSneakers.Amount);
                Assert.AreEqual("Running", sportSneakers.SportType);
            });
        }

        [Test]
        public void Constructor_EmptySportType_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new SportSneakers("Air Zoom", "Blue", 200.00m, 1.1, "Zoom Air", 43, 20, null));
        }

        [Test]
        public void Save_SportSneakersSavedToFile_FileShouldExist()
        {
            var sportSneakers = new SportSneakers("Air Zoom", "Blue", 200.00m, 1.1, "Zoom Air", 43, 20, "Running");

            SportSneakers.Save(_filePath);

            Assert.IsTrue(File.Exists(_filePath));
        }

        [Test]
        public void Load_FileDoesNotExist_ShouldNotThrowException()
        {
            Assert.DoesNotThrow(() => SportSneakers.Load("nonexistent.json"));
        }

        [Test]
        public void Load_ValidFile_ShouldLoadSportSneakers()
        {
            var sneakers1 = new SportSneakers("Air Zoom", "Blue", 200.00m, 1.1, "Zoom Air", 43, 20, "Running");
            var sneakers2 = new SportSneakers("Court Shoes", "White", 180.00m, 1.2, "Gel Cushion", 44, 15, "Tennis");

            SportSneakers.Save(_filePath);
            SportSneakers.Load(_filePath);

            var loadedSneakers = SportSneakers.GetAll();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(2, loadedSneakers.Count);
                Assert.AreEqual("Air Zoom", loadedSneakers[0].Name);
                Assert.AreEqual("Court Shoes", loadedSneakers[1].Name);
                Assert.AreEqual("Running", loadedSneakers[0].SportType);
                Assert.AreEqual("Tennis", loadedSneakers[1].SportType);
            });
        }

        [Test]
        public void ClearAll_ShouldRemoveAllSportSneakers()
        {
            var sportSneakers = new SportSneakers("Air Zoom", "Blue", 200.00m, 1.1, "Zoom Air", 43, 20, "Running");

            SportSneakers.ClearAll();

            Assert.AreEqual(0, SportSneakers.GetAll().Count);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            SportSneakers.ClearAll();
        }
    }
}