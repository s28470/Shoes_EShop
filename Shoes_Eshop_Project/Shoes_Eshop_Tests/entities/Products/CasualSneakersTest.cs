using NUnit.Framework;
using Shoes_Eshop_Project.Entities;
using Shoes_Eshop_Project.entities;
using System;
using System.IO;
using System.Collections.Generic;

namespace Shoes_Eshop_Project.Tests
{
    [TestFixture]
    public class CustomerTest
    {
        private string _filePath;
        private Address _address;

        [SetUp]
        public void Setup()
        {
            _filePath = Path.Combine(Path.GetTempPath(), "customers.json");
            _address = new Address("Warsaw", "Main Street", "123", null, "12345");
            Customer.ClearAll();
            IndividualCustomer.ClearAll();
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [Test]
        public void Constructor_ValidParameters_ShouldCreateCustomer()
        {
            var customer = new Customer("Alice Doe", "555555555", _address, "alice@example.com");
            Assert.AreEqual("Alice Doe", customer.Name);
            Assert.AreEqual("555555555", customer.ContactNumber);
            Assert.AreEqual(_address, customer.Address);
            Assert.AreEqual("alice@example.com", customer.Email);
        }

        [Test]
        public void Constructor_EmptyName_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Customer("", "123456789", _address, "test@example.com"));
        }

        [Test]
        public void Constructor_EmptyContactNumber_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Customer("Alice Doe", "", _address, "test@example.com"));
        }

        [Test]
        public void Save_CustomersSavedToFile_FileShouldExist()
        {
            var customer = new Customer("Alice Doe", "123456789", _address, "alice@example.com");
            Customer.Save(_filePath);
            Assert.IsTrue(File.Exists(_filePath));
        }

        [Test]
        public void Load_FileDoesNotExist_ShouldNotThrowException()
        {
            Assert.DoesNotThrow(() => Customer.Load("nonexistent.json"));
        }

        [Test]
        public void Load_ValidFile_ShouldLoadCustomers()
        {
            var customer1 = new Customer("Alice Doe", "123456789", _address, "alice@example.com");
            var customer2 = new Customer("Bob Smith", "987654321", _address, "bob@example.com");
            Customer.Save(_filePath);

            Customer.ClearAll();
            Customer.Load(_filePath);
            var loadedCustomers = Customer.GetAll();

            Assert.AreEqual(2, loadedCustomers.Count);
            Assert.AreEqual("Alice Doe", loadedCustomers[0].Name);
            Assert.AreEqual("Bob Smith", loadedCustomers[1].Name);
        }

        [Test]
        public void GetAll_ShouldReturnListOfCustomers()
        {
            var customer1 = new Customer("Alice Doe", "123456789", _address, "alice@example.com");
            var customer2 = new Customer("Bob Smith", "987654321", _address, "bob@example.com");
            var customers = Customer.GetAll();
            Assert.AreEqual(2, customers.Count);
        }

        [Test]
        public void ClearAll_ShouldRemoveAllCustomers()
        {
            var customer1 = new Customer("Alice Doe", "123456789", _address, "alice@example.com");
            var customer2 = new Customer("Bob Smith", "987654321", _address, "bob@example.com");
            Customer.ClearAll();
            var customers = Customer.GetAll();
            Assert.AreEqual(0, customers.Count);
        }

        [Test]
        public void ApplyDiscountForVip_ShouldApplyDiscountIfVip()
        {
            var customer = new Customer("Alice Doe", "123456789", _address, "alice@example.com");
            customer.TotalPurchases = 1200;
            customer.GetType().GetMethod("UpdateStatus", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(customer, null);
            var discountedPrice = customer.ApplyDiscountForVip(100);
            Assert.AreEqual(98, discountedPrice);
        }

        [Test]
        public void ApplyDiscountForVip_ShouldNotApplyDiscountIfNotVip()
        {
            var customer = new Customer("Alice Doe", "123456789", _address, "alice@example.com");
            customer.TotalPurchases = 800;
            var discountedPrice = customer.ApplyDiscountForVip(100);
            Assert.AreEqual(100, discountedPrice);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            Customer.ClearAll();
            IndividualCustomer.ClearAll();
        }
    }

    
}
