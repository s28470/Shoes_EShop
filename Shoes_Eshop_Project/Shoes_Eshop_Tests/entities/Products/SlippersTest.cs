using NUnit.Framework;
using Shoes_Eshop_Project.Entities;
using System;
using System.IO;
using System.Collections.Generic;

namespace Shoes_Eshop_Project.Tests
{
    [TestFixture]
    public class SlippersTests
    {
        private string _filePath;

        [SetUp]
        public void Setup()
        {
            _filePath = Path.Combine(Path.GetTempPath(), "slippers.json");
            Slippers.ClearAll();
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [Test]
        public void Constructor_ValidParameters_ShouldCreateSlippers()
        {
            var slippers = new Slippers("Winter Slippers", "Grey", 49.99m, "Non-slip", 42, 10);
            Assert.AreEqual("Winter Slippers", slippers.Name);
            Assert.AreEqual("Grey", slippers.Color);
            Assert.AreEqual(49.99m, slippers.Price);
            Assert.AreEqual("Non-slip", slippers.Grip);
            Assert.AreEqual(42, slippers.ShoeSize);
            Assert.AreEqual(10, slippers.Amount);
        }

        [Test]
        public void Constructor_EmptyGrip_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Slippers("Winter Slippers", "Grey", 49.99m, "", 42, 10));
        }

        [Test]
        public void Save_SlippersSavedToFile_FileShouldExist()
        {
            var slippers = new Slippers("Winter Slippers", "Grey", 49.99m, "Non-slip", 42, 10);
            Slippers.Save(_filePath);
            Assert.IsTrue(File.Exists(_filePath));
        }

        [Test]
        public void Load_FileDoesNotExist_ShouldNotThrowException()
        {
            Assert.DoesNotThrow(() => Slippers.Load("nonexistent.json"));
        }

        [Test]
        public void Load_ValidFile_ShouldLoadSlippers()
        {
            var slippers1 = new Slippers("Winter Slippers", "Grey", 49.99m, "Non-slip", 42, 10);
            var slippers2 = new Slippers("Summer Slippers", "Blue", 39.99m, "Anti-slip", 40, 5);
            Slippers.Save(_filePath);

            Slippers.ClearAll();
            Slippers.Load(_filePath);
            var loadedSlippers = Slippers.GetAll();

            Assert.AreEqual(2, loadedSlippers.Count);
            Assert.AreEqual("Winter Slippers", loadedSlippers[0].Name);
            Assert.AreEqual("Summer Slippers", loadedSlippers[1].Name);
            Assert.AreEqual("Non-slip", loadedSlippers[0].Grip);
            Assert.AreEqual("Anti-slip", loadedSlippers[1].Grip);
        }

        [Test]
        public void GetAll_ShouldReturnListOfSlippers()
        {
            var slippers1 = new Slippers("Winter Slippers", "Grey", 49.99m, "Non-slip", 42, 10);
            var slippers2 = new Slippers("Summer Slippers", "Blue", 39.99m, "Anti-slip", 40, 5);
            var slippersList = Slippers.GetAll();
            Assert.AreEqual(2, slippersList.Count);
        }

        [Test]
        public void ClearAll_ShouldRemoveAllSlippers()
        {
            var slippers1 = new Slippers("Winter Slippers", "Grey", 49.99m, "Non-slip", 42, 10);
            var slippers2 = new Slippers("Summer Slippers", "Blue", 39.99m, "Anti-slip", 40, 5);
            Slippers.ClearAll();
            var slippersList = Slippers.GetAll();
            Assert.AreEqual(0, slippersList.Count);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            Slippers.ClearAll();
        }
    }
}
