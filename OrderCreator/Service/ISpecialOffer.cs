using OrderCreator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
