using OrderCreator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCreator.DAL
{
    internal interface IProductRepository
    {
        public ReadOnlyCollection<Product> GetAvailableProducts();
    }
}
