using System.Text.Json.Serialization;

namespace OrderCreator.Model
{
    internal class Product
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public double Price { get; protected set; }

        public Product(string name, double price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
        }

        [JsonConstructor]
        public Product(Guid id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
