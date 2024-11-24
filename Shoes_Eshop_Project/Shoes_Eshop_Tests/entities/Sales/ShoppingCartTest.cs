using NUnit.Framework;
using Shoes_Eshop_Project.Entities.Sales;
using Shoes_Eshop_Project.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using Shoes_Eshop_Project.entities;

namespace Shoes_Eshop_Project.Tests.Sales
{
    [TestFixture]
    public class ShoppingCartTests
    {
        private string _filePath;

        [SetUp]
        public void Setup()
        {
            _filePath = Path.Combine(Path.GetTempPath(), "shoppingcarts.json");
            ShoppingCart.ClearAll();
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [Test]
        public void Constructor_ValidCustomer_ShouldCreateCart()
        {
            var customer = new IndividualCustomer("John Doe", "123456789", new Address("City", "Street", "1", "A", "12345"), "Male", 30);
            var cart = new ShoppingCart(customer);

            Assert.AreEqual(customer, cart.Customer);
        }

        [Test]
        public void Constructor_NullCustomer_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ShoppingCart(null));
        }

        [Test]
        public void AddProductToCart_ValidProduct_ShouldAddProduct()
        {
            var customer = new IndividualCustomer("John Doe", "123456789", new Address("City", "Street", "1", "A", "12345"), "Male", 30);
            var cart = new ShoppingCart(customer);
            var product = new Product("Sneakers", "Black", 100.00m);

            cart.AddProductToCart(product, 2);

            Assert.AreEqual(200.00m, cart.GetTotalPrice());
        }

        [Test]
        public void AddProductToCart_InvalidAmount_ShouldThrowArgumentException()
        {
            var customer = new IndividualCustomer("John Doe", "123456789", new Address("City", "Street", "1", "A", "12345"), "Male", 30);
            var cart = new ShoppingCart(customer);
            var product = new Product("Sneakers", "Black", 100.00m);

            Assert.Throws<ArgumentException>(() => cart.AddProductToCart(product, 0));
        }

        [Test]
        public void RemoveProduct_ValidProduct_ShouldRemoveProduct()
        {
            var customer = new IndividualCustomer("John Doe", "123456789", new Address("City", "Street", "1", "A", "12345"), "Male", 30);
            var cart = new ShoppingCart(customer);
            var product = new Product("Sneakers", "Black", 100.00m);

            cart.AddProductToCart(product, 2);
            cart.RemoveProduct(product);

            Assert.AreEqual(0, cart.GetTotalPrice());
        }

        [Test]
        public void RemoveProduct_ProductNotInCart_ShouldDoNothing()
        {
            var customer = new IndividualCustomer("John Doe", "123456789", new Address("City", "Street", "1", "A", "12345"), "Male", 30);
            var cart = new ShoppingCart(customer);
            var product = new Product("Sneakers", "Black", 100.00m);

            cart.RemoveProduct(product);

            Assert.AreEqual(0, cart.GetTotalPrice());
        }

        [Test]
        public void CompletePurchase_ShouldMarkCartAsCompleted()
        {
            var customer = new IndividualCustomer("John Doe", "123456789", new Address("City", "Street", "1", "A", "12345"), "Male", 30);
            var cart = new ShoppingCart(customer);

            cart.CompletePurchase();

            Assert.Throws<InvalidOperationException>(() => cart.AddProductToCart(new Product("Sneakers", "Black", 100.00m), 1));
        }

        [Test]
        public void Save_ShoppingCartSavedToFile_FileShouldExist()
        {
            var customer = new IndividualCustomer("John Doe", "123456789", new Address("City", "Street", "1", "A", "12345"), "Male", 30);
            var cart = new ShoppingCart(customer);

            ShoppingCart.Save(_filePath);

            Assert.IsTrue(File.Exists(_filePath));
        }

        [Test]
        public void Load_ValidFile_ShouldLoadShoppingCarts()
        {
            var customer = new IndividualCustomer("John Doe", "123456789", new Address("City", "Street", "1", "A", "12345"), "Male", 30);
            var cart = new ShoppingCart(customer);

            ShoppingCart.Save(_filePath);
            ShoppingCart.Load(_filePath);

            var loadedCarts = ShoppingCart.GetAll();

            Assert.AreEqual(1, loadedCarts.Count);
            Assert.AreEqual(customer.Name, loadedCarts[0].Customer.Name);
        }

        [Test]
        public void ClearAll_ShouldRemoveAllShoppingCarts()
        {
            var customer = new IndividualCustomer("John Doe", "123456789", new Address("City", "Street", "1", "A", "12345"), "Male", 30);
            var cart = new ShoppingCart(customer);

            ShoppingCart.ClearAll();

            Assert.AreEqual(0, ShoppingCart.GetAll().Count);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            ShoppingCart.ClearAll();
        }
    }
}