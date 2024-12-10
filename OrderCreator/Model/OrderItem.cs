using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCreator.Model
{
    internal class OrderItem
    {
        public double Price { get; protected set; }

        public OrderItem(double price)
        {
            Price = price;
        }
    }
}
