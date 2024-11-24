using NUnit.Framework;
using Shoes_Eshop_Project.Entities;
using System;
using System.IO;
using System.Collections.Generic;

namespace Shoes_Eshop_Project.Tests
{
    [TestFixture]
    public class CasualSneakersTests
    {
        private string _filePath;

        [SetUp]
        public void Setup()
        {
            _filePath = Path.Combine(Path.GetTempPath(), "casualSneakers.json");
            CasualSneakers.ClearAll();
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [Test]
        public void Constructor_ValidParameters_ShouldCreateCasualSneakers()
        {
            var casualSneakers = new CasualSneakers("Classic", "Summer");

            Assert.Multiple(() =>
            {
                Assert.AreEqual("Classic", casualSneakers.StyleType);
                Assert.AreEqual("Summer", casualSneakers.Season);
            });
        }

        [Test]
        public void Constructor_EmptyStyleType_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new CasualSneakers(null, "Summer"));
        }

        [Test]
        public void Constructor_EmptySeason_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new CasualSneakers("Classic", null));
        }

        [Test]
        public void Save_CasualSneakersSavedToFile_FileShouldExist()
        {
            var casualSneakers = new CasualSneakers("Classic", "Summer");

            CasualSneakers.Save(_filePath);

            Assert.IsTrue(File.Exists(_filePath));
        }

        [Test]
        public void Load_FileDoesNotExist_ShouldNotThrowException()
        {
            Assert.DoesNotThrow(() => CasualSneakers.Load("nonexistent.json"));
        }

        [Test]
        public void Load_ValidFile_ShouldLoadCasualSneakers()
        {
            var sneakers1 = new CasualSneakers("Classic", "Summer");
            var sneakers2 = new CasualSneakers("Trendy", "Winter");

            CasualSneakers.Save(_filePath);
            CasualSneakers.Load(_filePath);

            var loadedSneakers = CasualSneakers.GetAll();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(2, loadedSneakers.Count);
                Assert.AreEqual("Classic", loadedSneakers[0].StyleType);
                Assert.AreEqual("Trendy", loadedSneakers[1].StyleType);
                Assert.AreEqual("Summer", loadedSneakers[0].Season);
                Assert.AreEqual("Winter", loadedSneakers[1].Season);
            });
        }

        [Test]
        public void ClearAll_ShouldRemoveAllCasualSneakers()
        {
            var casualSneakers = new CasualSneakers("Classic", "Summer");

            CasualSneakers.ClearAll();

            Assert.AreEqual(0, CasualSneakers.GetAll().Count);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            CasualSneakers.ClearAll();
        }
    }
}