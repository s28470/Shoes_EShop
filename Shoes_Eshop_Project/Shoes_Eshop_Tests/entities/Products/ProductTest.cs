using NUnit.Framework;
using Shoes_Eshop_Project.Entities;
using System;

namespace Shoes_Eshop_Project.Tests
{
    [TestFixture]
    public class ProductTests
    {
        private class TestProduct : Product
        {
            public TestProduct(string name, string color, decimal price) : base(name, color, price) { }
        }

        [Test]
        public void Constructor_ValidParameters_ShouldCreateProduct()
        {
            var product = new TestProduct("Sneakers", "Black", 99.99m);

            Assert.Multiple(() =>
            {
                Assert.AreEqual("Sneakers", product.Name);
                Assert.AreEqual("Black", product.Color);
                Assert.AreEqual(99.99m, product.Price);
            });
        }

        [Test]
        public void Constructor_NullName_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new TestProduct(null, "Black", 99.99m));
        }

        [Test]
        public void Constructor_NegativePrice_ShouldThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new TestProduct("Sneakers", "Black", -1m));
        }

        [Test]
        public void Constructor_NullColor_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new TestProduct("Sneakers", null, 99.99m));
        }

        [Test]
        public void UpdateStock_ValidQuantity_ShouldUpdateAmount()
        {
            var product = new TestProduct("Sneakers", "Black", 99.99m) { Amount = 10 };

            product.UpdateStock(5);

            Assert.AreEqual(5, product.Amount);
        }

        [Test]
        public void UpdateStock_NegativeQuantity_ShouldThrowArgumentException()
        {
            var product = new TestProduct("Sneakers", "Black", 99.99m) { Amount = 10 };

            Assert.Throws<ArgumentException>(() => product.UpdateStock(-5));
        }

        [Test]
        public void UpdateStock_QuantityExceedsStock_ShouldThrowInvalidOperationException()
        {
            var product = new TestProduct("Sneakers", "Black", 99.99m) { Amount = 10 };

            Assert.Throws<InvalidOperationException>(() => product.UpdateStock(15));
        }
    }
}