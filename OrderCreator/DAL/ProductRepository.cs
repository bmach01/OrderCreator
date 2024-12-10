using OrderCreator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCreator.DAL
{
    internal class ProductRepository : IProductRepository
    {
        private readonly List<Product> _availableProducts;

        public ProductRepository() 
        {
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
