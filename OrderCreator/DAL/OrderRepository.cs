using OrderCreator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCreator.DAL
{
    internal class OrderRepository : IOrderRepository
    {
        public OrderRepository() { }

        public ReadOnlyCollection<Order> GetPreviousOrders()
        {
            // Get saved orders to present
            return new List<Order>().AsReadOnly();
        }

        public void SaveOrder(Order order)
        {
            // Here order would be sent to the server
        }
    }
}
