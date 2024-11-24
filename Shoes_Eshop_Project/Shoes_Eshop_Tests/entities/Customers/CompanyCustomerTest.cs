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
            _filePath = Path.Combine(Path.GetTempPath(), "companyCustomers.json");
            CompanyCustomer.ClearAll();
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [Test]
        public void Constructor_ValidOccupation_ShouldCreateCompanyCustomer()
        {
            var companyCustomer = new CompanyCustomer("Tech Company");
            Assert.AreEqual("Tech Company", companyCustomer.Occupation);
        }

        [Test]
        public void Constructor_NullOccupation_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new CompanyCustomer(null));
        }

        [Test]
        public void Websites_SetValidList_ShouldUpdateWebsites()
        {
            var companyCustomer = new CompanyCustomer("Tech Company");
            var websites = new List<string> { "https://example.com", "https://example.org" };
            companyCustomer.Websites = websites;

            Assert.AreEqual(websites, companyCustomer.Websites);
        }

        [Test]
        public void Websites_SetInvalidList_ShouldThrowArgumentException()
        {
            var companyCustomer = new CompanyCustomer("Tech Company");
            Assert.Throws<ArgumentException>(() => companyCustomer.Websites = new List<string> { "https://example.com", "" });
        }

        [Test]
        public void AddWebsite_ValidWebsite_ShouldAddWebsite()
        {
            var companyCustomer = new CompanyCustomer("Tech Company");
            companyCustomer.AddWebsite("https://example.com");

            Assert.Contains("https://example.com", companyCustomer.Websites);
        }

        [Test]
        public void AddWebsite_DuplicateWebsite_ShouldThrowInvalidOperationException()
        {
            var companyCustomer = new CompanyCustomer("Tech Company");
            companyCustomer.AddWebsite("https://example.com");

            Assert.Throws<InvalidOperationException>(() => companyCustomer.AddWebsite("https://example.com"));
        }

        [Test]
        public void RemoveWebsite_ExistingWebsite_ShouldRemoveWebsite()
        {
            var companyCustomer = new CompanyCustomer("Tech Company");
            companyCustomer.AddWebsite("https://example.com");
            companyCustomer.RemoveWebsite("https://example.com");

            Assert.IsFalse(companyCustomer.Websites.Contains("https://example.com"));
        }

        [Test]
        public void RemoveWebsite_NonExistingWebsite_ShouldThrowInvalidOperationException()
        {
            var companyCustomer = new CompanyCustomer("Tech Company");
            Assert.Throws<InvalidOperationException>(() => companyCustomer.RemoveWebsite("https://nonexistent.com"));
        }

        [Test]
        public void Save_CompanyCustomersSavedToFile_FileShouldExist()
        {
            var companyCustomer = new CompanyCustomer("Tech Company");
            CompanyCustomer.Save(_filePath);

            Assert.IsTrue(File.Exists(_filePath));
        }

        [Test]
        public void Load_FileDoesNotExist_ShouldNotThrowException()
        {
            Assert.DoesNotThrow(() => CompanyCustomer.Load("nonexistent.json"));
        }

        [Test]
        public void Load_ValidFile_ShouldLoadCompanyCustomers()
        {
            var companyCustomer1 = new CompanyCustomer("Tech Company");
            var companyCustomer2 = new CompanyCustomer("E-commerce Company");
            CompanyCustomer.Save(_filePath);

            CompanyCustomer.Load(_filePath);
            var loadedCompanyCustomers = CompanyCustomer.GetAll();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(2, loadedCompanyCustomers.Count);
                Assert.AreEqual("Tech Company", loadedCompanyCustomers[0].Occupation);
                Assert.AreEqual("E-commerce Company", loadedCompanyCustomers[1].Occupation);
            });
        }

        [Test]
        public void ClearAll_ShouldRemoveAllCompanyCustomers()
        {
            var companyCustomer1 = new CompanyCustomer("Tech Company");
            var companyCustomer2 = new CompanyCustomer("E-commerce Company");

            CompanyCustomer.ClearAll();

            Assert.AreEqual(0, CompanyCustomer.GetAll().Count);
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