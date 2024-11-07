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
            _filePath = Path.Combine(Path.GetTempPath(), "sport_sneakers.json");
            SportSneakers.ClearAll();
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [Test]
        public void Constructor_ValidParameters_ShouldCreateSportSneakers()
        {
            var sportSneakers = new SportSneakers("Basketball Shoes", "Red", 150.00m, 0.9, "Gel Cushion", 44, 20, "Basketball");
            Assert.AreEqual("Basketball Shoes", sportSneakers.Name);
            Assert.AreEqual("Red", sportSneakers.Color);
            Assert.AreEqual(150.00m, sportSneakers.Price);
            Assert.AreEqual(0.9, sportSneakers.Weight);
            Assert.AreEqual("Gel Cushion", sportSneakers.CushioningTechnology);
            Assert.AreEqual(44, sportSneakers.ShoeSize);
            Assert.AreEqual(20, sportSneakers.Amount);
            Assert.AreEqual("Basketball", sportSneakers.SportType);
        }

        [Test]
        public void Constructor_EmptySportType_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new SportSneakers("Basketball Shoes", "Red", 150.00m, 0.9, "Gel Cushion", 44, 20, ""));
        }

        [Test]
        public void Save_SportSneakersSavedToFile_FileShouldExist()
        {
            var sportSneakers = new SportSneakers("Basketball Shoes", "Red", 150.00m, 0.9, "Gel Cushion", 44, 20, "Basketball");
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
            var sneakers1 = new SportSneakers("Basketball Shoes", "Red", 150.00m, 0.9, "Gel Cushion", 44, 20, "Basketball");
            var sneakers2 = new SportSneakers("Running Shoes", "Blue", 130.00m, 0.8, "Foam Cushion", 42, 15, "Running");
            SportSneakers.Save(_filePath);

            SportSneakers.ClearAll();
            SportSneakers.Load(_filePath);
            var loadedSneakers = SportSneakers.GetAll();

            Assert.AreEqual(2, loadedSneakers.Count);
            Assert.AreEqual("Basketball Shoes", loadedSneakers[0].Name);
            Assert.AreEqual("Running Shoes", loadedSneakers[1].Name);
            Assert.AreEqual("Basketball", loadedSneakers[0].SportType);
            Assert.AreEqual("Running", loadedSneakers[1].SportType);
        }

        [Test]
        public void GetAll_ShouldReturnListOfSportSneakers()
        {
            var sneakers1 = new SportSneakers("Basketball Shoes", "Red", 150.00m, 0.9, "Gel Cushion", 44, 20, "Basketball");
            var sneakers2 = new SportSneakers("Running Shoes", "Blue", 130.00m, 0.8, "Foam Cushion", 42, 15, "Running");
            var sneakersList = SportSneakers.GetAll();
            Assert.AreEqual(2, sneakersList.Count);
        }

        [Test]
        public void ClearAll_ShouldRemoveAllSportSneakers()
        {
            var sneakers1 = new SportSneakers("Basketball Shoes", "Red", 150.00m, 0.9, "Gel Cushion", 44, 20, "Basketball");
            var sneakers2 = new SportSneakers("Running Shoes", "Blue", 130.00m, 0.8, "Foam Cushion", 42, 15, "Running");
            SportSneakers.ClearAll();
            var sneakersList = SportSneakers.GetAll();
            Assert.AreEqual(0, sneakersList.Count);
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
