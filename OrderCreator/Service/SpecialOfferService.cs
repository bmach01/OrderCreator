using OrderCreator.Model;

namespace OrderCreator.Service
{
    internal class SpecialOfferService
    {
        private LinkedList<ISpecialOffer> _specialOffers;

        public SpecialOfferService()
        {
            _specialOffers = new LinkedList<ISpecialOffer>();
        }

        public SpecialOfferService(LinkedList<ISpecialOffer> specialOffers)
        {
            _specialOffers = specialOffers;
        }

        public void AddSpecialOffer(ISpecialOffer specialOffer, ISpecialOffer before)
        {
            // Order matters
            var node = _specialOffers.Find(before);
            _specialOffers.AddBefore(node, specialOffer);

        }

        public void AppendSpecialOfffer(ISpecialOffer specialOffer)
        {
            _specialOffers.AddLast(specialOffer);
        }

        public void RemoveSpecialOffer(ISpecialOffer specialOffer)
        {
            _specialOffers.Remove(specialOffer);
        }

        public ref Order ApplyAllDiscounts(ref Order order)
        {
            bool appliedNonStackDiscount = false;

            foreach (var offer in _specialOffers)
            {
                // Skip non-stackable discounts if one has already been applied
                if (appliedNonStackDiscount && offer.type == SpecialOfferType.NON_STACKABLE)
                {
                    continue;
                }

                if (offer.IsValid(order))
                {
                    order.ApplyDiscount(offer.ApplyDiscount);

                    if (offer.type == SpecialOfferType.NON_STACKABLE)
                    {
                        appliedNonStackDiscount = true;
                    }
                }
            }

            return ref order;
        }
    }
}
