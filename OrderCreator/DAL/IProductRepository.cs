using OrderCreator.Model;
using System.Collections.ObjectModel;

namespace OrderCreator.DAL
{
    internal interface IProductRepository
    {
        public ReadOnlyCollection<Product> GetAvailableProducts();
    }
}
