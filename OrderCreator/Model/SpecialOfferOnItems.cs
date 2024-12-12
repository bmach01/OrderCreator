using System.Text.Json.Serialization;

namespace OrderCreator.Model
{
    internal class SpecialOfferOnItems : ISpecialOffer
    {
        public string Description { get; }
        public SpecialOfferType Type { get; }
        private double _minSum;
        private double _maxSum;
        private int _minCount;
        private int _maxCount;
        private double _discount;
        private ItemValue _onItem;

        [JsonConstructor]
        public SpecialOfferOnItems(string description)
        {
            Description = description;
        }

        public SpecialOfferOnItems(
            double minSum,
            double maxSum,
            int minCount,
            int maxCount,
            SpecialOfferType type,
            double discount,
            ItemValue onItem,
            string description
        )
        {
            _minSum = minSum;
            _maxSum = maxSum;
            _minCount = minCount;
            _maxCount = maxCount;
            Type = type;
            _discount = discount;
            _onItem = onItem;
            Description = description;
        }

        public double ApplyDiscount(Order order)
        {
            Product? item;

            if (_onItem == ItemValue.MOST_EXPENSIVE_ITEM)
                item = order.Items.MaxBy(it => it.Price);
            else
                item = order.Items.MinBy(it => it.Price);

            if (item == null) return order.DiscountedSum;

            double sum = order.DiscountedSum;

            sum -= item.Price * (_discount / 100);

            return sum;
        }

        public bool IsValid(Order order)
        {
            if (order.Sum > _maxSum || order.Sum < _minSum) return false;
            if (order.Items.Count > _maxCount || order.Items.Count < _minCount) return false;

            return true;
        }
    }
}
