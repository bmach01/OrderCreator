using OrderCreator.Model;
using System.Collections.ObjectModel;

namespace OrderCreator.DAL
{
    internal class SpecialOfferRepository : ISpecialOfferRepository
    {
        private readonly LinkedList<ISpecialOffer> _specialOffers;

        public SpecialOfferRepository()
        {
            // Order matters
            _specialOffers = new LinkedList<ISpecialOffer>();
            _specialOffers.AddFirst(
                new SpecialOfferOnAll(
                    5000d,
                    double.MaxValue,
                    0,
                    int.MaxValue,
                    SpecialOfferType.STACKABLE,
                    "Additional 5% off on minimum 5000PLN orders.",
                    true,
                    5
                )
            );
            _specialOffers.AddFirst(
                 new SpecialOfferOnItems(
                    0d,
                    double.MaxValue,
                    2,
                    int.MaxValue,
                    SpecialOfferType.NON_STACKABLE,
                    10,
                    ItemValue.CHEAPEST_ITEM,
                    "10% off on the cheaper item when buying two."
                )
            );

            _specialOffers.AddFirst(
                new SpecialOfferOnItems(
                    0d,
                    double.MaxValue,
                    3,
                    int.MaxValue,
                    SpecialOfferType.NON_STACKABLE,
                    20,
                    ItemValue.CHEAPEST_ITEM,
                    "20% off on the cheapest item when buying three."
                )
            );
        }

        public ReadOnlyCollection<ISpecialOffer> GetAvailableSpecialOffers()
        {
            return new ReadOnlyCollection<ISpecialOffer>(new List<ISpecialOffer>(_specialOffers));
        }
    }
}
