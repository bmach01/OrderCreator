using OrderCreator.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCreator.Model
{
    internal class Order
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public DateTime Created { get; }
        public List<Product> Items { get; private set; }
        public List<ISpecialOffer> AppliedDiscounts { get; private set; }
        public double Sum { get; private set; }

        public Order() 
        {
            Id = Guid.NewGuid();
            Created = DateTime.Now;
            Name = "Order_" + Id.ToString();
            Items = new List<Product> { };
            AppliedDiscounts= new List<ISpecialOffer>();
            Sum = 0;
        }

        public Order(String name) : this()
        {
            Name = name;
        }

        public Order(Guid id, string name, DateTime created, List<Product> items, List<ISpecialOffer> appliedDiscounts, double sum)
        {
            Id = id;
            Name = name;
            Created = created;
            Items = items;
            AppliedDiscounts = appliedDiscounts;
            Sum = sum;
        }

        public void AddItem(Product item)
        {
            Items.Add(item);
            Sum += item.Price;
        }

        public void RemoveItem(Product item)
        {
            if (Items.Remove(item))
                Sum -= item.Price;
        }

        public void RemoveItem(int index)
        {
            Sum -= Items[index].Price;
            Items.RemoveAt(index);
        }

        public void ApplyDiscount(Func<Order, double> discount)
        {
            Sum = discount(this);
        }

    }
}
