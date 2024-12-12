using OrderCreator.DAL;
using OrderCreator.Model;
using OrderCreator.Service;
using System.Collections.ObjectModel;

namespace OrderCreator.ViewModel
{
    internal class UniversalViewModel
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISpecialOfferRepository _specialOfferRepository;
        private readonly SpecialOfferService _specialOfferService;

        public UniversalViewModel(IOrderRepository orderRepository, IProductRepository productRepository, ISpecialOfferRepository specialOfferRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _specialOfferRepository = specialOfferRepository;

            _specialOfferService = new SpecialOfferService(new LinkedList<ISpecialOffer>(_specialOfferRepository.GetAvailableSpecialOffers()));
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

        public ReadOnlyCollection<ISpecialOffer> GetAllSpecialOffers()
        {
            return new ReadOnlyCollection<ISpecialOffer>(new List<ISpecialOffer>(_specialOfferService.SpecialOffers));
        }
    }
}
