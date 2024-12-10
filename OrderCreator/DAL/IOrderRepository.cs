using OrderCreator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCreator.DAL
{
    internal interface IOrderRepository
    {
        public void SaveOrder(Order order);

        public ReadOnlyCollection<Order> GetPreviousOrders();
    }
}
