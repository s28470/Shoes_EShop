using NUnit.Framework;
using Shoes_Eshop_Project.entities;
using Shoes_Eshop_Project.Entities;
using Shoes_Eshop_Project.Entities.Sales;

namespace Shoes_Eshop_Project.Tests
{
    [TestFixture]
    public class CustomerShoppingCartTests
    {
        [SetUp]
        public void SetUp()
        {
            Customer.ClearAll();
            ShoppingCart.ClearAll();
        }

        [Test]
        public void AddShoppingCart_AssociatesCartWithCustomer()
        {
            var customer = new Customer("John Doe", "1234567890", new Address("City", "Street", "123", "1A", "12345"));
            var cart = new ShoppingCart(customer);

            customer.AddShoppingCart(cart);

            Assert.Contains(cart, customer.GetShoppingCarts());
            Assert.AreEqual(customer, cart.Customer);
        }

        [Test]
        public void ShoppingCart_AssignsCustomerAutomatically()
        {
            var customer = new Customer("John Doe", "1234567890", new Address("City", "Street", "123", "1A", "12345"));
            var cart = new ShoppingCart(customer);

            Assert.Contains(cart, customer.GetShoppingCarts());
            Assert.AreEqual(customer, cart.Customer);
        }

        [Test]
        public void RemoveShoppingCart_RemovesAssociationFromBothSides()
        {
            var customer = new Customer("John Doe", "1234567890", new Address("City", "Street", "123", "1A", "12345"));
            var cart = new ShoppingCart(customer);
            customer.AddShoppingCart(cart);

            customer.RemoveShoppingCart(cart);

            Assert.IsFalse(customer.ShoppingCarts.Contains(cart));
            Assert.IsNull(cart.Customer);
        }

        [Test]
        public void ShoppingCart_UnsetsCustomerCorrectly()
        {
            var customer = new Customer("John Doe", "1234567890", new Address("City", "Street", "123", "1A", "12345"));
            var cart = new ShoppingCart(customer);

            cart.UnsetCustomer();

            Assert.IsNull(cart.Customer);
            Assert.IsFalse(customer.ShoppingCarts.Contains(cart));
        }

        [Test]
        public void AddShoppingCart_PreventsDuplicateAssociations()
        {
            var customer = new Customer("John Doe", "1234567890", new Address("City", "Street", "123", "1A", "12345"));
            var cart = new ShoppingCart(customer);
            customer.AddShoppingCart(cart);

            customer.AddShoppingCart(cart);

            Assert.AreEqual(1, customer.ShoppingCarts.Count);
        }

        [Test]
        public void ShoppingCart_AssociationWorksWithMultipleCarts()
        {
            var customer = new Customer("John Doe", "1234567890", new Address("City", "Street", "123", "1A", "12345"));
            var cart1 = new ShoppingCart(customer);
            var cart2 = new ShoppingCart(customer);

            Assert.Contains(cart1, customer.GetShoppingCarts());
            Assert.Contains(cart2, customer.GetShoppingCarts());
            Assert.AreEqual(customer, cart1.Customer);
            Assert.AreEqual(customer, cart2.Customer);
        }

        [Test]
        public void Customer_RemoveDoesNotAffectOtherCarts()
        {
            var customer = new Customer("John Doe", "1234567890", new Address("City", "Street", "123", "1A", "12345"));
            var cart1 = new ShoppingCart(customer);
            var cart2 = new ShoppingCart(customer);

            customer.RemoveShoppingCart(cart1);

            Assert.IsFalse(customer.ShoppingCarts.Contains(cart1));
            Assert.Contains(cart2, customer.GetShoppingCarts());
            Assert.IsNull(cart1.Customer);
            Assert.AreEqual(customer, cart2.Customer);
        }

        [Test]
        public void Customer_RemovalCleansUpShoppingCartsFromInstances()
        {
            var customer = new Customer("John Doe", "1234567890", new Address("City", "Street", "123", "1A", "12345"));
            var cart1 = new ShoppingCart(customer);
            var cart2 = new ShoppingCart(customer);

            Customer.Remove(customer);

            Assert.IsFalse(Customer.GetAll().Contains(customer));
            Assert.IsFalse(ShoppingCart.GetAll().Contains(cart1));
            Assert.IsFalse(ShoppingCart.GetAll().Contains(cart2));
        }
    }
}