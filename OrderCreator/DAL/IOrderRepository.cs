using OrderCreator.Model;
using System.Collections.ObjectModel;

namespace OrderCreator.DAL
{
    internal interface IOrderRepository
    {
        public void SaveOrder(Order order);

        public ReadOnlyCollection<Order> GetPreviousOrders();
    }
}
