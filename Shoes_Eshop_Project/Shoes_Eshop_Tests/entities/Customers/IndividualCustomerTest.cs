using NUnit.Framework;
using Shoes_Eshop_Project.Entities;
using Shoes_Eshop_Project.entities;
using System;
using System.IO;
using System.Collections.Generic;

namespace Shoes_Eshop_Project.Tests
{
    [TestFixture]
    public class IndividualCustomerTests
    {
        private string _filePath;

        [SetUp]
        public void Setup()
        {
            _filePath = Path.Combine(Path.GetTempPath(), "individualCustomers.json");
            IndividualCustomer.ClearAll();
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [Test]
        public void Constructor_ValidParameters_ShouldCreateIndividualCustomer()
        {
            var address = new Address("Warsaw", "Main Street", "123", null, "12345");
            var customer = new IndividualCustomer("John Doe", "123456789", address, "Male", 30, "john.doe@example.com");

            Assert.Multiple(() =>
            {
                Assert.AreEqual("John Doe", customer.Name);
                Assert.AreEqual("123456789", customer.ContactNumber);
                Assert.AreEqual(address, customer.Address);
                Assert.AreEqual("Male", customer.Gender);
                Assert.AreEqual(30, customer.Age);
                Assert.AreEqual("john.doe@example.com", customer.Email);
            });
        }

        [Test]
        public void Constructor_InvalidGender_ShouldThrowArgumentException()
        {
            var address = new Address("Warsaw", "Main Street", "123", null, "12345");
            Assert.Throws<ArgumentException>(() => new IndividualCustomer("John Doe", "123456789", address, null));
        }

        [Test]
        public void Constructor_InvalidAge_ShouldThrowArgumentOutOfRangeException()
        {
            var address = new Address("Warsaw", "Main Street", "123", null, "12345");
            Assert.Throws<ArgumentOutOfRangeException>(() => new IndividualCustomer("John Doe", "123456789", address, "Male", -1));
        }

        [Test]
        public void Save_IndividualCustomersSavedToFile_FileShouldExist()
        {
            var address = new Address("Warsaw", "Main Street", "123", null, "12345");
            var customer = new IndividualCustomer("John Doe", "123456789", address, "Male", 30, "john.doe@example.com");

            IndividualCustomer.Save(_filePath);

            Assert.IsTrue(File.Exists(_filePath));
        }

        [Test]
        public void Load_FileDoesNotExist_ShouldNotThrowException()
        {
            Assert.DoesNotThrow(() => IndividualCustomer.Load("nonexistent.json"));
        }

        [Test]
        public void Load_ValidFile_ShouldLoadIndividualCustomers()
        {
            var address1 = new Address("Warsaw", "Main Street", "123", null, "12345");
            var customer1 = new IndividualCustomer("John Doe", "123456789", address1, "Male", 30, "john.doe@example.com");
            var address2 = new Address("Krakow", "Another Street", "456", "12", "67890");
            var customer2 = new IndividualCustomer("Jane Doe", "987654321", address2, "Female", 25, "jane.doe@example.com");

            IndividualCustomer.Save(_filePath);
            IndividualCustomer.Load(_filePath);

            var loadedCustomers = IndividualCustomer.GetAll();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(2, loadedCustomers.Count);
                Assert.AreEqual("John Doe", loadedCustomers[0].Name);
                Assert.AreEqual("Jane Doe", loadedCustomers[1].Name);
                Assert.AreEqual(30, loadedCustomers[0].Age);
                Assert.AreEqual(25, loadedCustomers[1].Age);
            });
        }

        [Test]
        public void ClearAll_ShouldRemoveAllIndividualCustomers()
        {
            var address = new Address("Warsaw", "Main Street", "123", null, "12345");
            var customer = new IndividualCustomer("John Doe", "123456789", address, "Male", 30, "john.doe@example.com");

            IndividualCustomer.ClearAll();

            Assert.AreEqual(0, IndividualCustomer.GetAll().Count);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            IndividualCustomer.ClearAll();
        }
    }
}