using OrderCreator.Model;
using System.Collections.ObjectModel;

namespace OrderCreator.DAL
{
    internal class ProductRepository : IProductRepository
    {
        private readonly List<Product> _availableProducts;

        public ProductRepository()
        {
            // Prop products
            _availableProducts = new List<Product>
            {
                new Product("Laptop", 2500),
                new Product("Klawiatura", 150),
                new Product("Mysz", 90),
                new Product("Monitor", 1000),
                new Product("Kaczka debuggująca", 66)
            };
        }

        public ReadOnlyCollection<Product> GetAvailableProducts()
        {
            return _availableProducts.AsReadOnly();
        }
    }
}
