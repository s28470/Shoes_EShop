using NUnit.Framework;
using Shoes_Eshop_Project.Entities;
using Shoes_Eshop_Project.Entities.Sales;
using System;
using Shoes_Eshop_Project.entities;

namespace Shoes_Eshop_Project.Tests
{
    [TestFixture]
    public class ProductShoppingCartTests
    {
        private Product product;
        private ShoppingCart cart;
        private Customer customer;

        [SetUp]
        public void Setup()
        {
            product = new Product("Sneakers", "Blue", 120);
            product.Amount = 5;

            ShoppingCart.ClearAll();
            Address _address1 = new Address("City1", "Street1", "12A", "34", "12345");
            customer = new Customer("Jane Doe", "jane@example.com", _address1);
            cart = new ShoppingCart(customer);
        }

        [Test]
        public void AddProductToCart_CreatesAssociation()
        {
            cart.AddProductToCart(product, 2);

            Assert.IsTrue(product.GetShoppingCartsWithThisProduct().Contains(cart));
            Assert.AreEqual(1, product.GetShoppingCartsWithThisProduct().Count);
        }

        [Test]
        public void RemoveProductFromCart_RemovesAssociation()
        {
            cart.AddProductToCart(product, 2);
            cart.RemoveProduct(product);

            Assert.IsFalse(product.GetShoppingCartsWithThisProduct().Contains(cart));
            Assert.AreEqual(0, product.GetShoppingCartsWithThisProduct().Count);
        }

        [Test]
        public void AddProductToCart_AfterCompletePurchase_ThrowsException()
        {
            cart.CompletePurchase();
            Assert.Throws<InvalidOperationException>(() => cart.AddProductToCart(product, 2));
        }

        [Test]
        public void RemoveProductFromCart_AfterCompletePurchase_ThrowsException()
        {
            cart.AddProductToCart(product, 2);
            cart.CompletePurchase();
            Assert.Throws<InvalidOperationException>(() => cart.RemoveProduct(product));
        }

        [Test]
        public void AddProductToCart_WithNegativeAmount_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => cart.AddProductToCart(product, -1));
        }

        [Test]
        public void AddProductToCart_InsufficientStock_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => product.UpdateStock(10));
        }

        [Test]
        public void GetTotalPrice_ReturnsCorrectValue()
        {
            cart.AddProductToCart(product, 2);
            Assert.AreEqual(240, cart.GetTotalPrice());
        }

        [Test]
        public void ClearAll_ClearsAllShoppingCarts()
        {
            ShoppingCart.ClearAll();
            Assert.AreEqual(0, ShoppingCart.GetAll().Count);
        }
        
    }
}