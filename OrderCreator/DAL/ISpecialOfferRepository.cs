using OrderCreator.Model;
using System.Collections.ObjectModel;

namespace OrderCreator.DAL
{
    internal interface ISpecialOfferRepository
    {
        public ReadOnlyCollection<ISpecialOffer> GetAvailableSpecialOffers();
    }
}
