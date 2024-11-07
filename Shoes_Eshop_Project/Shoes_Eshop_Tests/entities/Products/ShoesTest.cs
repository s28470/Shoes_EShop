using NUnit.Framework;
using Shoes_Eshop_Project.Entities;
using Shoes_Eshop_Project.entities;
using System;
using System.Collections.Generic;

namespace Shoes_Eshop_Project.Tests
{
    [TestFixture]
    public class ShoesWithLeatherTypeTests
    {
        [Test]
        public void Constructor_ValidLeatherType_ShouldSetLeatherType()
        {
            var shoes = new Shoes("Stylish Boots", "Brown", 150.00m, LeatherType.FullGrainLeather, 42, 5);
            Assert.AreEqual(LeatherType.FullGrainLeather, shoes.LeatherType);
        }

        [Test]
        public void LeatherType_ShouldIncludeAllEnumValues()
        {
            var leatherTypes = Enum.GetValues(typeof(LeatherType));
            Assert.AreEqual(3, leatherTypes.Length);
            Assert.Contains(LeatherType.FullGrainLeather, leatherTypes);
            Assert.Contains(LeatherType.Suede, leatherTypes);
            Assert.Contains(LeatherType.VeganLeather, leatherTypes);
        }

        [Test]
        public void GetAll_ShouldReturnAllShoesInstances()
        {
            Shoes.ClearAll();
            var shoes1 = new Shoes("Classic Shoes", "Black", 99.99m, LeatherType.FullGrainLeather, 42, 10);
            var shoes2 = new Shoes("Eco-friendly Sneakers", "Green", 89.99m, LeatherType.VeganLeather, 40, 8);
            var shoesList = Shoes.GetAll();
            Assert.AreEqual(2, shoesList.Count);
        }

        [Test]
        public void ClearAll_ShouldRemoveAllShoesInstances()
        {
            var shoes1 = new Shoes("Classic Shoes", "Black", 99.99m, LeatherType.Suede, 42, 10);
            var shoes2 = new Shoes("Eco-friendly Sneakers", "Green", 89.99m, LeatherType.VeganLeather, 40, 8);
            Shoes.ClearAll();
            var shoesList = Shoes.GetAll();
            Assert.AreEqual(0, shoesList.Count);
        }
    }
}