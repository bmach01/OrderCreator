using System.Text.Json.Serialization;

namespace OrderCreator.Model
{
    internal class SpecialOfferOnAll : ISpecialOffer
    {
        private double _minSum;
        private double _maxSum;
        private int _minCount;
        private int _maxCount;
        public SpecialOfferType Type { get; }
        public string Description { get; }
        private bool _percentageValue;
        private double _discount;

        [JsonConstructor]
        public SpecialOfferOnAll(string description)
        {
            Description = description;
        }

        public SpecialOfferOnAll(
            double minSum, 
            double maxSum, 
            int minCount, 
            int maxCount, 
            SpecialOfferType type, 
            string description,
            bool percentageValue, 
            double discount
        )
        {
            _minSum = minSum;
            _maxSum = maxSum;
            _minCount = minCount;
            _maxCount = maxCount;
            Type = type;
            Description = description;
            _percentageValue = percentageValue;
            _discount = discount;
        }

        public double ApplyDiscount(Order order)
        {
            if (_percentageValue)
                return order.DiscountedSum * (100 - _discount) / 100;

            return order.DiscountedSum - _discount;
        }

        public bool IsValid(Order order)
        {
            Console.WriteLine(order.Sum + " <== order sum");

            if (order.Sum > _maxSum || order.Sum < _minSum) return false;
            if (order.Items.Count > _maxCount || order.Items.Count < _minCount) return false;

            return true;
        }

    }

        
}
