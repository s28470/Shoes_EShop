using NUnit.Framework;
using Shoes_Eshop_Project.Entities;
using Shoes_Eshop_Project.entities;

namespace Shoes_Eshop_Project.Tests
{
    [TestFixture]
    public class CustomerAddressTests
    {
        [SetUp]
        public void SetUp()
        {
            Address.ClearAll();
            Customer.ClearAll();
        }

        [Test]
        public void RemoveAddress_RemovesAssociationFromCustomerAndAddress()
        {
            
            var address = new Address("City", "Street", "123", null, "12345");
            var customer = new Customer("John Doe", "1234567890", address);

            
            customer.RemoveAddress();

            
            Assert.IsNull(customer.Address);
            Assert.IsFalse(address.HasCustomer());
        }

        [Test]
        public void RemoveCustomer_RemovesAssociationFromAddressAndCustomer()
        {
           
            var address = new Address("City", "Street", "123", null, "12345");
            var customer = new Customer("John Doe", "1234567890", address);

           
            address.RemoveCustomer();

         
            Assert.IsNull(customer.Address);
            Assert.IsFalse(address.HasCustomer());
        }

        [Test]
        public void Address_CanBeReassignedAfterRemovingCustomer()
        {
         
            var address1 = new Address("City1", "Street1", "123", null, "12345");
            var address2 = new Address("City2", "Street2", "456", null, "67890");
            var customer = new Customer("John Doe", "1234567890", address1);

         
            address1.RemoveCustomer();
            customer.Address = address2;

         
            Assert.AreEqual(address2, customer.Address);
            Assert.IsFalse(address1.HasCustomer());
            Assert.IsTrue(address2.HasCustomer());
        }

        [Test]
        public void Customer_CanBeReassignedToNewAddress()
        {
          
            var address1 = new Address("City1", "Street1", "123", null, "12345");
            var address2 = new Address("City2", "Street2", "456", null, "67890");
            var customer = new Customer("John Doe", "1234567890", address1);

          
            customer.RemoveAddress();
            customer.Address = address2;

          
            Assert.AreEqual(address2, customer.Address);
            Assert.IsFalse(address1.HasCustomer());
            Assert.IsTrue(address2.HasCustomer());
        }

        [Test]
        public void RemovingAddressDoesNotAffectOtherCustomers()
        {
          
            var address1 = new Address("City1", "Street1", "123", null, "12345");
            var address2 = new Address("City2", "Street2", "456", null, "67890");
            var customer1 = new Customer("John Doe", "1234567890", address1);
            var customer2 = new Customer("Jane Doe", "0987654321", address2);

          
            customer1.RemoveAddress();

          
            Assert.IsNull(customer1.Address);
            Assert.IsTrue(address2.HasCustomer());
            Assert.AreEqual(customer2, address2.GetCustomer());
        }

        [Test]
        public void RemovingCustomerDoesNotAffectOtherAddresses()
        {
          
            var address1 = new Address("City1", "Street1", "123", null, "12345");
            var address2 = new Address("City2", "Street2", "456", null, "67890");
            var customer1 = new Customer("John Doe", "1234567890", address1);
            var customer2 = new Customer("Jane Doe", "0987654321", address2);

          
            address1.RemoveCustomer();

          
            Assert.IsNull(customer1.Address);
            Assert.AreEqual(customer2, address2.GetCustomer());
            Assert.IsTrue(address2.HasCustomer());
        }

        [Test]
        public void RemoveAddress_WhenNoAddressSet_DoesNothing()
        {
          
            var address = new Address("City", "Street", "123", null, "12345");
            var customer = new Customer("John Doe", "1234567890", address);

          
            customer.RemoveAddress(); 
            customer.RemoveAddress(); 

          
            Assert.IsNull(customer.Address); 
        }

        [Test]
        public void RemoveCustomer_WhenNoCustomerSet_DoesNothing()
        {
          
            var address = new Address("City", "Street", "123", null, "12345");

          
            address.RemoveCustomer();

            
            Assert.IsFalse(address.HasCustomer());
        }
        
        [Test]
        public void Remove_RemovesAddressFromInstancesWhenCustomerIsDeleted()
        {
            var address = new Address("City", "Street", "123", "1A", "12345");
            var customer = new Customer("John Doe", "1234567890", address);

            Customer.Remove(customer);

            Assert.IsFalse(Address.GetAll().Contains(address), "Address was not removed from instances when customer was deleted.");
        }
    }
}