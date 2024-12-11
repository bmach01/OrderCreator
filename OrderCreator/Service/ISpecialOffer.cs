using OrderCreator.Model;

namespace OrderCreator.Service
{
    internal interface ISpecialOffer
    {
        public SpecialOfferType type { get; }
        public string Description { get; }
        public bool IsValid(Order order);
        public double ApplyDiscount(Order order);
    }
}
