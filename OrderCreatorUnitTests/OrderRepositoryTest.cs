using OrderCreator.DAL;
using OrderCreator.Model;
using System.Text.Json;

namespace OrderCreatorUnitTests
{
    [TestClass]
    public class OrderRepositoryTests
    {
        private string testFilePath;

        [TestInitialize]
        public void SetUp()
        {
            testFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + "OrderCreatorDBProp.txt");
            if (File.Exists(testFilePath))
            {
                File.Delete(testFilePath);
            }
        }

        [TestCleanup]
        public void TearDown()
        {
            if (File.Exists(testFilePath))
            {
                File.Delete(testFilePath);
            }
        }

        [TestMethod]
        public void GetPreviousOrders_ShouldReturnEmptyList_WhenFileIsEmpty()
        {
            // Arrange
            var orderRepository = new OrderRepository(testFilePath);

            // Act
            var orders = orderRepository.GetPreviousOrders();

            // Assert
            Assert.AreEqual(0, orders.Count);
        }

        [TestMethod]
        public void GetPreviousOrders_ShouldReturnOrders()
        {
            // Arrange
            var sampleOrders = new List<Order>
            {
                new Order
                (
                    id: Guid.NewGuid(),
                    name: "test1",
                    created: DateTime.Now,
                    items: new List<Product>(new Product[] {new Product("i1", 150.50), new Product("i2", 200) }),
                    appliedDiscounts: new List<ISpecialOffer>(),
                    sum: 350.50,
                    discountedSum: 350.50
                ),
                new Order
                (
                    id: Guid.NewGuid(),
                    name: "test2",
                    created: DateTime.Now,
                    items: new List<Product>(new Product[] {new Product("1i", 1150.25), new Product("2i", 2130) }),
                    appliedDiscounts: new List<ISpecialOffer>(),
                    sum: 3280.20,
                    discountedSum: 3280.20
                )
            };

            var serializedOrders = JsonSerializer.Serialize(sampleOrders);
            File.WriteAllText(testFilePath, serializedOrders);

            var orderRepository = new OrderRepository(testFilePath);

            // Act
            var orders = orderRepository.GetPreviousOrders();

            // Assert
            Assert.AreEqual(2, orders.Count);
            Assert.AreEqual(sampleOrders[0].Name, orders[0].Name);
            Assert.AreEqual(sampleOrders[1].Name, orders[1].Name);
        }

        [TestMethod]
        public void SaveOrder_ShouldSaveOrderToFile()
        {
            // Arrange
            var orderRepository = new OrderRepository(testFilePath);
            var newOrder = new Order
            (
                id: Guid.NewGuid(),
                name: "test1",
                created: DateTime.Now,
                items: new List<Product>(new Product[] { new Product("i1", 150.50), new Product("i2", 200) }),
                appliedDiscounts: new List<ISpecialOffer>(),
                sum: 350.50,
                discountedSum: 350.50
            );

            // Act
            orderRepository.SaveOrder(newOrder);

            // Assert
            var fileContent = File.ReadAllText(testFilePath);
            var savedOrders = JsonSerializer.Deserialize<List<Order>>(fileContent);

            Assert.IsNotNull(savedOrders);
            Assert.AreEqual(1, savedOrders.Count);
            Assert.AreEqual(newOrder.Name, savedOrders[0].Name);
        }

        [TestMethod]
        public void SaveOrder_ShouldAppendOrderHistory()
        {
            // Arrange
            var orderRepository = new OrderRepository(testFilePath);
            var initialOrder = new Order
            (
                id: Guid.NewGuid(),
                name: "test1",
                created: DateTime.Now,
                items: new List<Product>(new Product[] { new Product("i1", 150.50), new Product("i2", 200) }),
                appliedDiscounts: new List<ISpecialOffer>(),
                sum: 350.50,
                discountedSum: 350.50
            );
            orderRepository.SaveOrder(initialOrder);

            // Act
            var secondOrder = new Order
            (
                id: Guid.NewGuid(),
                name: "test2",
                created: DateTime.Now,
                items: new List<Product>(new Product[] { new Product("1i", 1150.25), new Product("2i", 2130) }),
                appliedDiscounts: new List<ISpecialOffer>(),
                sum: 3280.20,
                discountedSum: 3280.20
            );
            orderRepository.SaveOrder(secondOrder);

            // Assert
            var fileContent = File.ReadAllText(testFilePath);
            var savedOrders = JsonSerializer.Deserialize<List<Order>>(fileContent);

            Assert.IsNotNull(savedOrders);
            Assert.AreEqual(2, savedOrders.Count);
            Assert.AreEqual(initialOrder.Name, savedOrders[0].Name);
            Assert.AreEqual(secondOrder.Name, savedOrders[1].Name);
        }
    }
}
