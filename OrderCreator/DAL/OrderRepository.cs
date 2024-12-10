using OrderCreator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderCreator.DAL
{
    internal class OrderRepository : IOrderRepository
    {
        private static readonly string _dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "OrderCreatorDBProp.txt"
        );

        public OrderRepository() 
        {
            if (!File.Exists(_dbPath))
            {
                using (File.Create(_dbPath)) { }
            }
        }

        public ReadOnlyCollection<Order> GetPreviousOrders()
        {
            try
            {
                string fileContent = File.ReadAllText(_dbPath);

                if (string.IsNullOrWhiteSpace(fileContent))
                {
                    return new ReadOnlyCollection<Order>(new List<Order>());
                }

                var orders = JsonSerializer.Deserialize<List<Order>>(fileContent);
                return orders?.AsReadOnly() ?? new ReadOnlyCollection<Order>(new List<Order>());
            }
            catch (Exception ex)
            {
                throw new IOException("Failed to retrieve previous orders.", ex);
            }
        }

        public void SaveOrder(Order order)
        {
            // Here order would be normally sent to the server
            // For the sake of the exercise, it will be turned to JSON and stored in text file
            try
            {
                List<Order> orders;

                if (File.Exists(_dbPath))
                {
                    string fileContent = File.ReadAllText(_dbPath);
                    orders = string.IsNullOrWhiteSpace(fileContent)
                        ? new List<Order>()
                        : JsonSerializer.Deserialize<List<Order>>(fileContent) ?? new List<Order>();
                }
                else
                {
                    orders = new List<Order>();
                }
                orders.Add(order);

                File.WriteAllText(_dbPath, JsonSerializer.Serialize(orders));
            }
            catch (Exception ex)
            {
                throw new IOException("Failed to save the order.", ex);
            }
        }
    }
}
