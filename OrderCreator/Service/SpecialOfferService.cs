using OrderCreator.Model;

namespace OrderCreator.Service
{
    internal class SpecialOfferService
    {
        public LinkedList<ISpecialOffer> SpecialOffers { get; private set; }

        public SpecialOfferService()
        {
            SpecialOffers = new LinkedList<ISpecialOffer>();
        }

        public SpecialOfferService(LinkedList<ISpecialOffer> specialOffers)
        {
            SpecialOffers = specialOffers;
        }

        public void AddSpecialOffer(ISpecialOffer specialOffer, ISpecialOffer before)
        {
            // Order matters
            var node = SpecialOffers.Find(before);
            SpecialOffers.AddBefore(node, specialOffer);

        }

        public void AppendSpecialOfffer(ISpecialOffer specialOffer)
        {
            SpecialOffers.AddLast(specialOffer);
        }

        public void RemoveSpecialOffer(ISpecialOffer specialOffer)
        {
            SpecialOffers.Remove(specialOffer);
        }

        public ref Order ApplyAllDiscounts(ref Order order)
        {
            bool appliedNonStackDiscount = false;

            foreach (var offer in SpecialOffers)
            {
                // Skip non-stackable discounts if one has already been applied
                if (appliedNonStackDiscount && offer.Type == SpecialOfferType.NON_STACKABLE)
                {
                    continue;
                }

                if (offer.IsValid(order))
                {
                    order.ApplyDiscount(offer);

                    if (offer.Type == SpecialOfferType.NON_STACKABLE)
                    {
                        appliedNonStackDiscount = true;
                    }
                }
            }

            return ref order;
        }
    }
}
