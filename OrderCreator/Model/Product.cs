using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCreator.Model
{
    internal class Product : OrderItem
    {
        public String Name { get; private set; }

        public Product(string name, double price) : base(price)
        {
            Name = name;
        }
    }
}
