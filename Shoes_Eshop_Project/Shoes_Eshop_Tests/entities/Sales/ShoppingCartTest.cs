using NUnit.Framework;
using Shoes_Eshop_Project.Entities;
using Shoes_Eshop_Project.Entities.Sales;
using System;
using System.IO;
using System.Collections.Generic;
using Shoes_Eshop_Project.entities;

namespace Shoes_Eshop_Project.Tests
{
    [TestFixture]
    public class ShoppingCartTests
    {
        private string _filePath;
        private Customer _customer;
        private Product _product;

        [SetUp]
        public void Setup()
        {
            _filePath = Path.Combine(Path.GetTempPath(), "shopping_carts.json");
            _customer = new Customer("Alice Doe", "555555555", new Address("Warsaw", "Main Street", "123", null, "12345"));
            _product = new Slippers("Summer Slippers", "Blue", 39.99m, "Anti-slip", 40, 5);
            ShoppingCart.ClearAll();
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [Test]
        public void Constructor_ValidCustomer_ShouldCreateShoppingCart()
        {
            var cart = new ShoppingCart(_customer);
            Assert.AreEqual(_customer, cart.Customer);
        }

        [Test]
        public void Constructor_NullCustomer_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ShoppingCart(null));
        }

        [Test]
        public void AddProductToCart_ValidProduct_ShouldAddProduct()
        {
            var cart = new ShoppingCart(_customer);
            cart.AddProductToCart(_product, 2);
            Assert.AreEqual(2 * _product.Price, cart.GetTotalPrice());
        }

        [Test]
        public void AddProductToCart_CompletedCart_ShouldThrowInvalidOperationException()
        {
            var cart = new ShoppingCart(_customer);
            cart.CompletePurchase();
            Assert.Throws<InvalidOperationException>(() => cart.AddProductToCart(_product, 1));
        }

        [Test]
        public void AddProductToCart_NonPositiveAmount_ShouldThrowArgumentException()
        {
            var cart = new ShoppingCart(_customer);
            Assert.Throws<ArgumentException>(() => cart.AddProductToCart(_product, 0));
        }

        [Test]
        public void RemoveProduct_ShouldRemoveProductFromCart()
        {
            var cart = new ShoppingCart(_customer);
            cart.AddProductToCart(_product, 2);
            cart.RemoveProduct(_product);
            Assert.AreEqual(0, cart.GetTotalPrice());
        }

        [Test]
        public void CompletePurchase_ShouldSetCartAsCompleted()
        {
            var cart = new ShoppingCart(_customer);
            cart.CompletePurchase();
            Assert.Throws<InvalidOperationException>(() => cart.AddProductToCart(_product, 1));
            Assert.Throws<InvalidOperationException>(() => cart.RemoveProduct(_product));
        }

        [Test]
        public void GetTotalPrice_ShouldReturnCorrectTotalPrice()
        {
            var cart = new ShoppingCart(_customer);
            var product2 = new Slippers("Summer Slippers", "Blue", 39.99m, "Anti-slip", 40, 5);
            cart.AddProductToCart(_product, 2);
            cart.AddProductToCart(product2, 1);
            Assert.AreEqual((2 * _product.Price) + product2.Price, cart.GetTotalPrice());
        }

        [Test]
        public void Save_ShoppingCartsSavedToFile_FileShouldExist()
        {
            var cart = new ShoppingCart(_customer);
            ShoppingCart.Save(_filePath);
            Assert.IsTrue(File.Exists(_filePath));
        }

        [Test]
        public void Load_FileDoesNotExist_ShouldNotThrowException()
        {
            Assert.DoesNotThrow(() => ShoppingCart.Load("nonexistent.json"));
        }

        [Test]
        public void Load_ValidFile_ShouldLoadShoppingCarts()
        {
            var cart1 = new ShoppingCart(_customer);
            var cart2 = new ShoppingCart(new Customer("Bob Doe", "555555555", new Address("Warsaw", "Main Street", "123", null, "12345")));
            ShoppingCart.Save(_filePath);

            ShoppingCart.ClearAll();
            ShoppingCart.Load(_filePath);
            var loadedCarts = ShoppingCart.GetAll();

            Assert.AreEqual(2, loadedCarts.Count);
            Assert.AreEqual("Alice Doe", loadedCarts[0].Customer.Name);
            Assert.AreEqual("Bob Doe", loadedCarts[1].Customer.Name);
        }

        [Test]
        public void GetAll_ShouldReturnListOfShoppingCarts()
        {
            var cart1 = new ShoppingCart(_customer);
            var cart2 = new ShoppingCart(new Customer("Bob Doe", "555555555", new Address("Warsaw", "Main Street", "123", null, "12345")));
            var carts = ShoppingCart.GetAll();
            Assert.AreEqual(2, carts.Count);
        }

        [Test]
        public void ClearAll_ShouldRemoveAllShoppingCarts()
        {
            var cart1 = new ShoppingCart(_customer);
            var cart2 = new ShoppingCart(new Customer("Bob Doe", "555555555", new Address("Warsaw", "Main Street", "123", null, "12345")));
            ShoppingCart.ClearAll();
            var carts = ShoppingCart.GetAll();
            Assert.AreEqual(0, carts.Count);
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
