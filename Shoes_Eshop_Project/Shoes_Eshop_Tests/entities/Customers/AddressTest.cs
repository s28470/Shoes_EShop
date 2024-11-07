using NUnit.Framework;
using Shoes_Eshop_Project.entities;
using System;
using System.IO;
using System.Collections.Generic;

namespace Shoes_Eshop_Project.Tests
{
    [TestFixture]
    public class AddressTests
    {
        private string _filePath;

        [SetUp]
        public void Setup()
        {
            _filePath = Path.Combine(Path.GetTempPath(), "addresses.json");
            Address.ClearAll();
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [Test]
        public void Constructor_ValidParameters_ShouldCreateAddress()
        {
            var address = new Address("Warsaw", "Main Street", "123", null, "12345");
            Assert.AreEqual("Warsaw", address.City);
            Assert.AreEqual("Main Street", address.Street);
            Assert.AreEqual("123", address.HouseNumber);
            Assert.AreEqual(null, address.ApartmentNumber);
            Assert.AreEqual("12345", address.PostalCode);
        }

        [Test]
        public void Constructor_InvalidPostalCode_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Address("Warsaw", "Main Street", "123", null, "1234"));
        }

        [Test]
        public void Constructor_EmptyCity_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Address("", "Main Street", "123", null, "12345"));
        }

        [Test]
        public void Save_AddressesSavedToFile_FileShouldExist()
        {
            var address = new Address("Warsaw", "Main Street", "123", null, "12345");
            Address.Save(_filePath);
            Assert.IsTrue(File.Exists(_filePath));
        }

        [Test]
        public void Load_FileDoesNotExist_ShouldThrowFileNotFoundException()
        {
            Assert.Throws<FileNotFoundException>(() => Address.Load("nonexistent.json"));
        }

        [Test]
        public void Load_ValidFile_ShouldLoadAddresses()
        {
            var address1 = new Address("Warsaw", "Main Street", "123", null, "12345");
            var address2 = new Address("Krakow", "Another Street", "456", "12", "67890");
            Address.Save(_filePath);

            Address.Load(_filePath);
            var loadedAddresses = Address.GetAll();

            Assert.AreEqual(2, loadedAddresses.Count);
            Assert.AreEqual("Warsaw", loadedAddresses[0].City);
            Assert.AreEqual("Krakow", loadedAddresses[1].City);
        }

        [Test]
        public void GetAll_ShouldReturnListOfAddresses()
        {
            var address1 = new Address("Warsaw", "Main Street", "123", null, "12345");
            var address2 = new Address("Krakow", "Another Street", "456", "12", "67890");
            var addresses = Address.GetAll();
            Assert.AreEqual(2, addresses.Count);
        }

        [Test]
        public void ClearAll_ShouldRemoveAllAddresses()
        {
            var address1 = new Address("Warsaw", "Main Street", "123", null, "12345");
            var address2 = new Address("Krakow", "Another Street", "456", "12", "67890");
            Address.ClearAll();
            var addresses = Address.GetAll();
            Assert.AreEqual(0, addresses.Count);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            Address.ClearAll();
        }
    }
}
