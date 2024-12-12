using OrderCreator.Model;

namespace OrderCreatorUnitTests
{
    [TestClass]
    public class OrderTest
    {
        [TestMethod]
        public void SetNewSavedDate_UpdatesCreatedDate()
        {
            // Arrange
            Order order = new Order();
            DateTime oldDate = order.Created;

            // Act
            order.SetNewSavedDate();

            // Assert
            Assert.IsTrue(order.Created > oldDate);
        }

        [TestMethod]
        public void AddItem_RemoveItem_CorrectSum()
        {
            // Arrange
            Order order = new Order();
            double expectedSum = 0;

            // Act & Assert
            for (int i = 0; i < 10; i++)
            {
                Product product = new Product("item" + i, i * 100);
                order.AddItem(product);
                expectedSum += i * 100;
                Assert.IsTrue(expectedSum == order.Sum);

                order.RemoveItem(product);
                expectedSum -= i * 100;
                Assert.IsTrue(expectedSum == order.Sum);
            }
        }
    }
}