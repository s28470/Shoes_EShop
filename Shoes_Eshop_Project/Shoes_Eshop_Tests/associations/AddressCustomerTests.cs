using NUnit.Framework;
using Shoes_Eshop_Project.entities;
using Shoes_Eshop_Project.Entities;
using System;

namespace Shoes_Eshop_Project.Tests
{
    [TestFixture]
    public class AddressCustomerTests
    {
        private Address _address1;
        private Address _address2;
        private Customer _customer1;
        private Customer _customer2;

        [SetUp]
        public void Setup()
        {
            Address.ClearAll();
            Customer.ClearAll();

            _address1 = new Address("City1", "Street1", "12A", "34", "12345");
            _address2 = new Address("City2", "Street2", "15B", null, "54321");

            _customer1 = new Customer("John Doe", "1234567890", _address1, "john@example.com");
            _customer2 = new Customer("Jane Doe", "0987654321", _address1);
        }

        [Test]
        public void Test_CreationOfReferences_CheckReverseConnection()
        {
            Assert.Contains(_customer1, _address1.GetCustomersWithAddress());
            Assert.Contains(_customer2, _address1.GetCustomersWithAddress());
            Assert.AreEqual(_address1, _customer1.Address);
        }

        [Test]
        public void Test_ModificationOfReferences_CheckReverseConnection()
        {
            // Change Customer1's address
            _customer1.Address = _address2;

            Assert.Contains(_customer1, _address2.GetCustomersWithAddress());
            Assert.IsFalse(_address1.GetCustomersWithAddress().Contains(_customer1));
            Assert.AreEqual(_address2, _customer1.Address);
        }

        [Test]
        public void Test_DeletionOfReferences_CheckReverseConnection()
        {
            _address1.RemoveCustomer(_customer2);

            Assert.IsFalse(_address1.GetCustomersWithAddress().Contains(_customer2));
            Assert.DoesNotThrow(() => _customer2.Address = _address2);
        }
        

        [Test]
        public void Test_Exceptions_Validation()
        {
            Assert.Throws<ArgumentException>(() => new Address("", "Street", "10", null, "12345"));
            Assert.Throws<ArgumentException>(() => new Address("City", "", "10", null, "12345"));
            Assert.Throws<ArgumentException>(() => new Address("City", "Street", "", null, "12345"));
            Assert.Throws<ArgumentException>(() => new Address("City", "Street", "10", null, "InvalidPostal"));
            Assert.Throws<ArgumentException>(() => new Customer("", "123456", _address1));
            Assert.Throws<ArgumentNullException>(() => new Customer("John", "123456", null));
        }
    }
}