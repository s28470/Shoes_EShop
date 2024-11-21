using NUnit.Framework;
using Shoes_Eshop_Project.Entities;
using System;
using System.IO;
using System.Collections.Generic;

namespace Shoes_Eshop_Project.Tests
{
    [TestFixture]
    public class CompanyCustomerTests
    {
        private string _filePath;

        [SetUp]
        public void Setup()
        {
            _filePath = Path.Combine(Path.GetTempPath(), "company_customers.json");
            CompanyCustomer.ClearAll();
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [Test]
        public void Constructor_ValidParameters_ShouldCreateCompanyCustomer()
        {
            var customer = new CompanyCustomer("IT Services");
            Assert.AreEqual("IT Services", customer.Occupation);
        }

        [Test]
        public void Constructor_EmptyOccupation_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new CompanyCustomer(""));
        }

        
        [Test]
        public void Save_CustomersSavedToFile_FileShouldExist()
        {
            var customer = new CompanyCustomer("IT Services");
            CompanyCustomer.Save(_filePath);
            Assert.IsTrue(File.Exists(_filePath));
        }

        [Test]
        public void Load_FileDoesNotExist_ShouldNotThrowException()
        {
            Assert.DoesNotThrow(() => CompanyCustomer.Load("nonexistent.json"));
        }

        [Test]
        public void Load_ValidFile_ShouldLoadCustomers()
        {
            var customer1 = new CompanyCustomer("IT Services");
            var customer2 = new CompanyCustomer("Consulting");
            CompanyCustomer.Save(_filePath);

            CompanyCustomer.ClearAll();
            CompanyCustomer.Load(_filePath);
            var loadedCustomers = CompanyCustomer.GetAll();

            Assert.AreEqual(2, loadedCustomers.Count);
            Assert.AreEqual("IT Services", loadedCustomers[0].Occupation);
            Assert.AreEqual("Consulting", loadedCustomers[1].Occupation);
        }

        [Test]
        public void GetAll_ShouldReturnListOfCustomers()
        {
            var customer1 = new CompanyCustomer("IT Services");
            var customer2 = new CompanyCustomer("Consulting");
            var customers = CompanyCustomer.GetAll();
            Assert.AreEqual(2, customers.Count); 
        }

        [Test]
        public void ClearAll_ShouldRemoveAllCustomers()
        {
            var customer1 = new CompanyCustomer("IT Services");
            var customer2 = new CompanyCustomer("Consulting");
            CompanyCustomer.ClearAll();
            var customers = CompanyCustomer.GetAll();
            Assert.AreEqual(0, customers.Count);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            CompanyCustomer.ClearAll();
        }
    }
}
