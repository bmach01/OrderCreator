using OrderCreator.Service;
using System.Text.Json.Serialization;

namespace OrderCreator.Model
{
    internal class Order
    {
        public Guid Id { get; }
        public string Name { get; }
        public DateTime Created { get; private set; }
        public List<Product> Items { get; private set; } // Map should also be considered, especially for large orders
        public List<ISpecialOffer> AppliedDiscounts { get; private set; }
        public double Sum { get; private set; }

        public Order()
        {
            Id = Guid.NewGuid();
            Created = DateTime.Now;
            Name = "Order_" + Id.ToString();
            Items = new List<Product> { };
            AppliedDiscounts = new List<ISpecialOffer>();
            Sum = 0;
        }

        public Order(String name) : this()
        {
            Name = name;
        }

        [JsonConstructor]
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

        public bool HasItem(Product item)
        {
            return Items.Contains(item);
        }

        public void SetNewSavedDate()
        {
            Created = DateTime.Now;
        }

    }
}
