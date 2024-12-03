using NUnit.Framework;
using System;
using System.Collections.Generic;
using Shoes_Eshop_Project.Entities;
using Shoes_Eshop_Project.Entities.Sales;

[TestFixture]
public class PromotionProductAssociationTests
{
    private Product _product;
    private Promotion _promotion;

    [SetUp]
    public void Setup()
    {
        _product = new Product("Test Product", "Red", 100);
        _promotion = new Promotion(
            "Test Promotion",
            DateTime.Now.AddDays(-1),
            DateTime.Now.AddDays(1),
            new List<Product> { _product }
        );
    }

    [Test]
    public void AddPromotionToProduct_ShouldCreateAssociation()
    {
        var newPromotion = new Promotion(
            "New Promotion",
            DateTime.Now,
            DateTime.Now.AddDays(10),
            new List<Product> { _product }
        );

        Assert.Contains(newPromotion, (System.Collections.ICollection)_product.GetPromotions());
        Assert.IsTrue(newPromotion.HasProduct(_product));
    }

    [Test]
    public void RemovePromotionFromProduct_ShouldDeleteAssociation()
    {
        _product.RemovePromotion(_promotion);

        Assert.IsFalse(_product.HasPromotion(_promotion));
        Assert.IsFalse(_promotion.HasProduct(_product));
    }

    [Test]
    public void UpdatePromotionForProduct_ShouldReplaceOldPromotionWithNew()
    {
        var newPromotion = new Promotion(
            "Updated Promotion",
            DateTime.Now,
            DateTime.Now.AddDays(10),
            new List<Product>()
        );

        _product.UpdatePromotion(_promotion, newPromotion);

        Assert.IsFalse(_product.HasPromotion(_promotion));
        Assert.IsTrue(_product.HasPromotion(newPromotion));
        Assert.IsTrue(newPromotion.HasProduct(_product));
        Assert.IsFalse(_promotion.HasProduct(_product));
    }

    [Test]
    public void GetPromotionsFromProduct_ShouldReturnAllAssociatedPromotions()
    {
        var anotherPromotion = new Promotion(
            "Another Promotion",
            DateTime.Now,
            DateTime.Now.AddDays(10),
            new List<Product> { _product }
        );

        var promotions = _product.GetPromotions();
        Assert.Contains(_promotion, (System.Collections.ICollection)promotions);
        Assert.Contains(anotherPromotion, (System.Collections.ICollection)promotions);
    }

    [Test]
    public void HasPromotionInProduct_ShouldReturnTrueIfPromotionExists()
    {
        Assert.IsTrue(_product.HasPromotion(_promotion));
    }

    [Test]
    public void AddProductToPromotion_ShouldCreateAssociation()
    {
        var newProduct = new Product("Another Product", "Blue", 50);
        _promotion.AddProduct(newProduct);

        Assert.Contains(newProduct, (System.Collections.ICollection)_promotion.GetProducts());
        Assert.IsTrue(newProduct.HasPromotion(_promotion));
    }

    [Test]
    public void RemoveProductFromPromotion_ShouldDeleteAssociation()
    {
        _promotion.RemoveProduct(_product);

        Assert.IsFalse(_promotion.HasProduct(_product));
        Assert.IsFalse(_product.HasPromotion(_promotion));
    }

    [Test]
    public void PromotionAutoRemoveWhenNoProductsLeft_ShouldRemovePromotion()
    {
        _promotion.RemoveProduct(_product);

        Assert.IsFalse(Promotion.GetAll().Contains(_promotion));
    }

    [Test]
    public void HasProductInPromotion_ShouldReturnTrueIfProductExists()
    {
        Assert.IsTrue(_promotion.HasProduct(_product));
    }

    [Test]
    public void GetProductsFromPromotion_ShouldReturnAllAssociatedProducts()
    {
        var anotherProduct = new Product("Another Product", "Green", 70);
        _promotion.AddProduct(anotherProduct);

        var products = _promotion.GetProducts();
        Assert.Contains(_product, (System.Collections.ICollection)products);
        Assert.Contains(anotherProduct, (System.Collections.ICollection)products);
    }

    [Test]
    public void BidirectionalAssociationWhenAddingPromotion_ShouldMaintainConsistency()
    {
        var newPromotion = new Promotion(
            "Bidirectional Test",
            DateTime.Now,
            DateTime.Now.AddDays(5),
            new List<Product> { _product }
        );

        Assert.IsTrue(_product.HasPromotion(newPromotion));
        Assert.IsTrue(newPromotion.HasProduct(_product));
    }

    [Test]
    public void BidirectionalAssociationWhenRemovingPromotion_ShouldMaintainConsistency()
    {
        _promotion.RemoveProduct(_product);

        Assert.IsFalse(_promotion.HasProduct(_product));
        Assert.IsFalse(_product.HasPromotion(_promotion));
    }

    [Test]
    public void BidirectionalAssociationWhenAddingProduct_ShouldMaintainConsistency()
    {
        var newProduct = new Product("Bidirectional Product", "Yellow", 120);
        _promotion.AddProduct(newProduct);

        Assert.IsTrue(_promotion.HasProduct(newProduct));
        Assert.IsTrue(newProduct.HasPromotion(_promotion));
    }

    [Test]
    public void BidirectionalAssociationWhenRemovingProduct_ShouldMaintainConsistency()
    {
        _promotion.RemoveProduct(_product);

        Assert.IsFalse(_promotion.HasProduct(_product));
        Assert.IsFalse(_product.HasPromotion(_promotion));
    }
}