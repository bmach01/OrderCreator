using OrderCreator.DAL;
using OrderCreator.Model;
using OrderCreator.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCreator.ViewModel
{
    internal class UniversalViewModel
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly SpecialOfferService _specialOfferService;

        public UniversalViewModel(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _specialOfferService = new SpecialOfferService();
        }

        public ReadOnlyCollection<Order> GetHistory() 
        { 
            return _orderRepository.GetPreviousOrders();
        }

        public ReadOnlyCollection<Product> GetProducts()
        { 
            return _productRepository.GetAvailableProducts();
        }

        public void SaveOrder(Order order) 
        { 
            _orderRepository.SaveOrder(order);
        }

        public Order ApplyDiscounts(Order order)
        {
            return _specialOfferService.ApplyAllDiscounts(ref order);
        }
    }
}
