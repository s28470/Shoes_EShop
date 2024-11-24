using NUnit.Framework;
using Shoes_Eshop_Project.Entities;
using Shoes_Eshop_Project.entities;
using System;
using System.IO;
using System.Collections.Generic;

namespace Shoes_Eshop_Project.Tests
{
    [TestFixture]
    public class CustomerTests
    {
        private string _filePath;

        [SetUp]
        public void Setup()
        {
            _filePath = Path.Combine(Path.GetTempPath(), "customers.json");
            Customer.ClearAll();
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [Test]
        public void Constructor_ValidParameters_ShouldCreateCustomer()
        {
            var address = new Address("Warsaw", "Main Street", "123", null, "12345");
            var customer = new TestCustomer("John Doe", "123456789", address, "john.doe@example.com");

            Assert.Multiple(() =>
            {
                Assert.AreEqual("John Doe", customer.Name);
                Assert.AreEqual("123456789", customer.ContactNumber);
                Assert.AreEqual(address, customer.Address);
                Assert.AreEqual("john.doe@example.com", customer.Email);
                Assert.AreEqual(CustomerStatus.Default, customer.CustomerStatus);
            });
        }

        [Test]
        public void Constructor_InvalidName_ShouldThrowArgumentException()
        {
            var address = new Address("Warsaw", "Main Street", "123", null, "12345");
            Assert.Throws<ArgumentException>(() => new TestCustomer(null, "123456789", address));
        }

        [Test]
        public void TotalPurchases_ExceedsThreshold_ShouldUpdateToVIP()
        {
            var address = new Address("Warsaw", "Main Street", "123", null, "12345");
            var customer = new TestCustomer("John Doe", "123456789", address);

            customer.TotalPurchases = 1500;

            Assert.AreEqual(CustomerStatus.VIP, customer.CustomerStatus);
        }

        [Test]
        public void ApplyDiscountForVip_CustomerIsVIP_ShouldApplyDiscount()
        {
            var address = new Address("Warsaw", "Main Street", "123", null, "12345");
            var customer = new TestCustomer("John Doe", "123456789", address);

            customer.TotalPurchases = 1500; // Become VIP
            double discountedPrice = customer.ApplyDiscountForVip(100);

            Assert.AreEqual(98, discountedPrice);
        }

        [Test]
        public void ApplyDiscountForVip_CustomerIsNotVIP_ShouldNotApplyDiscount()
        {
            var address = new Address("Warsaw", "Main Street", "123", null, "12345");
            var customer = new TestCustomer("John Doe", "123456789", address);

            double price = customer.ApplyDiscountForVip(100);

            Assert.AreEqual(100, price);
        }

        [Test]
        public void Save_CustomersSavedToFile_FileShouldExist()
        {
            var address = new Address("Warsaw", "Main Street", "123", null, "12345");
            var customer = new TestCustomer("John Doe", "123456789", address);

            Customer.Save(_filePath);

            Assert.IsTrue(File.Exists(_filePath));
        }

        [Test]
        public void Load_FileDoesNotExist_ShouldNotThrowException()
        {
            Assert.DoesNotThrow(() => Customer.Load("nonexistent.json"));
        }
        

        [Test]
        public void ClearAll_ShouldRemoveAllCustomers()
        {
            var address = new Address("Warsaw", "Main Street", "123", null, "12345");
            var customer = new TestCustomer("John Doe", "123456789", address);

            Customer.ClearAll();

            Assert.AreEqual(0, Customer.GetAll().Count);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            Customer.ClearAll();
        }

        private class TestCustomer : Customer
        {
            public TestCustomer(string name, string contactNumber, Address address, string? email = null)
                : base(name, contactNumber, address, email)
            {
            }
        }
    }
}