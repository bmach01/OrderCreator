using OrderCreator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCreator.Service
{
    internal interface SpecialOffer
    {
        public bool IsValid(Order order);
        public double GenerateNewSum(Order order);
    }
}
