using OrderCreator.Model;
using OrderCreator.Service;

namespace OrderCreatorUnitTests
{
    [TestClass]
    public class SpecialOfferServiceTests
    {
        [TestMethod]
        public void AppendSpecialOffer_ShouldMaintainCorrectOrder()
        {
            // Arrange
            var service = new SpecialOfferService();
            var offer1 = new SpecialOfferOnAll("test1");
            var offer2 = new SpecialOfferOnAll("test2");

            // Act
            service.AppendSpecialOfffer(offer1);
            service.AppendSpecialOfffer(offer2);

            // Assert
            Assert.AreEqual(offer1, service.SpecialOffers.First.Value);
            Assert.AreEqual(offer2, service.SpecialOffers.Last.Value);
        }

        [TestMethod]
        public void AddSpecialOffer_ShouldInsertAtCorrectPosition()
        {
            // Arrange
            var service = new SpecialOfferService();
            var offer1 = new SpecialOfferOnAll("test1");
            var offer2 = new SpecialOfferOnAll("test2");
            var offer3 = new SpecialOfferOnAll("test3");

            service.AppendSpecialOfffer(offer1);
            service.AppendSpecialOfffer(offer3);

            // Act
            service.AddSpecialOffer(offer2, offer1);

            // Assert
            Assert.AreEqual(offer2, service.SpecialOffers.First.Value);
            Assert.AreEqual(offer1, service.SpecialOffers.First.Next.Value);
            Assert.AreEqual(offer3, service.SpecialOffers.Last.Value);
        }

        [TestMethod]
        public void ApplyDiscount_ShouldApplyCorrectDiscounts()
        {
            // Arrange
            var offers = new LinkedList<ISpecialOffer>(new ISpecialOffer[]
            {
                new SpecialOfferOnItems(0d, double.MaxValue, 3, int.MaxValue, SpecialOfferType.NON_STACKABLE, 20, ItemValue.CHEAPEST_ITEM, "20% off on cheapest item"),
                new SpecialOfferOnItems(0d, double.MaxValue, 2, int.MaxValue, SpecialOfferType.NON_STACKABLE, 10, ItemValue.CHEAPEST_ITEM, "10% off on cheaper item"),
                new SpecialOfferOnAll(5000d, double.MaxValue, 0, int.MaxValue, SpecialOfferType.STACKABLE, "5% off", true, 5)
            });

            var service = new SpecialOfferService(offers);
            var order = new Order();

            // Act & Assert
            order.AddItem(new Product("item1", 1000));
            service.ApplyAllDiscounts(ref order);
            Assert.AreEqual(order.Sum, order.DiscountedSum, "No discounts on single item.");

            order.AddItem(new Product("item2", 500));
            service.ApplyAllDiscounts(ref order);
            Assert.AreEqual(order.Sum - 50, order.DiscountedSum, "10% discount on cheapest item for two items.");

            var product3 = new Product("item3", 1500);
            order.AddItem(product3);
            service.ApplyAllDiscounts(ref order);
            Assert.AreEqual(order.Sum - 100, order.DiscountedSum, "20% discount on cheapest item for three items.");

            order.RemoveItem(product3);
            service.ApplyAllDiscounts(ref order);
            Assert.AreEqual(order.Sum - 50, order.DiscountedSum, "Revert to 10% discount after item removal.");

            order.AddItem(new Product("item3.2", 10000));
            service.ApplyAllDiscounts(ref order);
            Assert.AreEqual((order.Sum - 100) * 0.95, order.DiscountedSum, "Apply 5% stackable discount on total.");
        }
    }
}
