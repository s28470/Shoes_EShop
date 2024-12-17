using NUnit.Framework;
using System;
using System.Collections.Generic;
using Shoes_Eshop_Project.Entities;
using Shoes_Eshop_Project.Entities.Sales;

[TestFixture]
public class PromotionAssociationTests
{
    private Promotion _promotionA;
    private Promotion _promotionB;

    [SetUp]
    public void Setup()
    {
        _promotionA = new Promotion(
            "Promotion A",
            DateTime.Now.AddDays(-1),
            DateTime.Now.AddDays(1),
            new List<Product>()
        );

        _promotionB = new Promotion(
            "Promotion B",
            DateTime.Now,
            DateTime.Now.AddDays(2),
            new List<Product>()
        );
    }

    [Test]
    public void AddMainPromotion_ShouldSetBidirectionalAssociation()
    {
        _promotionA.AddMainPromotion(_promotionB);

        Assert.AreEqual(_promotionB, _promotionA.MainPromotion);
        Assert.Contains(_promotionA, (System.Collections.ICollection)_promotionB.GetRelatedPromotions());
    }

    [Test]
    public void RemoveMainPromotion_ShouldClearBidirectionalAssociation()
    {
        _promotionA.AddMainPromotion(_promotionB);
        _promotionA.RemoveMainPromotion();

        Assert.IsNull(_promotionA.MainPromotion);
        Assert.IsFalse(_promotionB.GetRelatedPromotions().Contains(_promotionA));
    }

    [Test]
    public void AddRelatedPromotion_ShouldSetBidirectionalAssociation()
    {
        _promotionB.AddRelatedPromotion(_promotionA);

        Assert.AreEqual(_promotionB, _promotionA.MainPromotion);
        Assert.Contains(_promotionA, (System.Collections.ICollection)_promotionB.GetRelatedPromotions());
    }

    [Test]
    public void RemoveRelatedPromotion_ShouldClearBidirectionalAssociation()
    {
        _promotionB.AddRelatedPromotion(_promotionA);
        _promotionB.RemoveRelatedPromotion(_promotionA);

        Assert.IsNull(_promotionA.MainPromotion);
        Assert.IsFalse(_promotionB.GetRelatedPromotions().Contains(_promotionA));
    }

    [Test]
    public void ChangeMainPromotion_ShouldReplaceOldMainPromotionWithNew()
    {
        var promotionC = new Promotion(
            "Promotion C",
            DateTime.Now,
            DateTime.Now.AddDays(3),
            new List<Product>()
        );

        _promotionA.AddMainPromotion(_promotionB);
        _promotionA.ChangeMainPromotion(promotionC);

        Assert.AreEqual(promotionC, _promotionA.MainPromotion);
        Assert.IsFalse(_promotionB.GetRelatedPromotions().Contains(_promotionA));
        Assert.Contains(_promotionA, (System.Collections.ICollection)promotionC.GetRelatedPromotions());
    }

    [Test]
    public void GetRelatedPromotions_ShouldReturnAllAssociatedPromotions()
    {
        var promotionC = new Promotion(
            "Promotion C",
            DateTime.Now,
            DateTime.Now.AddDays(3),
            new List<Product>()
        );

        _promotionB.AddRelatedPromotion(_promotionA);
        _promotionB.AddRelatedPromotion(promotionC);

        var relatedPromotions = _promotionB.GetRelatedPromotions();

        Assert.Contains(_promotionA, (System.Collections.ICollection)relatedPromotions);
        Assert.Contains(promotionC, (System.Collections.ICollection)relatedPromotions);
    }

    [Test]
    public void BidirectionalAssociationWhenAddingMainPromotion_ShouldMaintainConsistency()
    {
        _promotionA.AddMainPromotion(_promotionB);

        Assert.AreEqual(_promotionB, _promotionA.MainPromotion);
        Assert.Contains(_promotionA, (System.Collections.ICollection)_promotionB.GetRelatedPromotions());
    }

    [Test]
    public void BidirectionalAssociationWhenRemovingMainPromotion_ShouldMaintainConsistency()
    {
        _promotionA.AddMainPromotion(_promotionB);
        _promotionA.RemoveMainPromotion();

        Assert.IsNull(_promotionA.MainPromotion);
        Assert.IsFalse(_promotionB.GetRelatedPromotions().Contains(_promotionA));
    }

    [Test]
    public void AddingSameMainPromotionMultipleTimes_ShouldNotDuplicate()
    {
        _promotionA.AddMainPromotion(_promotionB);
        _promotionA.AddMainPromotion(_promotionB);

        Assert.AreEqual(_promotionB, _promotionA.MainPromotion);
        Assert.AreEqual(1, _promotionB.GetRelatedPromotions().Count);
    }

    [Test]
    public void RemovingNonExistingRelatedPromotion_ShouldNotThrowException()
    {
        Assert.DoesNotThrow(() => _promotionB.RemoveRelatedPromotion(_promotionA));
    }
}