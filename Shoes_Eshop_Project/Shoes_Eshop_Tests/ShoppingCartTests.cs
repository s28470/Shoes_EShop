using Shoes_Eshop_Project.entities;
using Shoes_Eshop_Project.entities.Sales;

namespace Shoes_Eshop_Tests
{
    [TestFixture]
    public class ShoppingCartTests
    {
        [Test]
        public void TestValidatePostalCode()
        {
            var address = new Address();
            bool isValid = address.ValidatePostalCode("12345");
            Assert.IsTrue(isValid);

            isValid = address.ValidatePostalCode("1234a");
            Assert.IsFalse(isValid);
        }

        [Test]
        public void TestApplyDiscountForVip()
        {
            var customer = new Customer()
            {
                TotalPurchases = 1200
            };

            double totalPrice = 100;
            var discountedPrice = customer.applyDiscountForVip(totalPrice);

            Assert.AreEqual(98, discountedPrice);
        }

        [Test]
        public void TestIsPromotionActive()
        {
            var promotion = new Promotion
            {
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(1)
            };

            bool isActive = promotion.IsPromotionActive();

            Assert.IsTrue(isActive);
        }

        [Test]
        public void TestUpdateStock()
        {
            var product = new Shoes()
            {
                Name = "Sneakers",
                Amount = 10
            };

            product.UpdateStock(3);

            Assert.AreEqual(7, product.Amount);
        }
    }
}
