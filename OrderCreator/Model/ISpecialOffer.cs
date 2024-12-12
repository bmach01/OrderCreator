using System.Text.Json.Serialization;

namespace OrderCreator.Model
{
    [JsonDerivedType(typeof(SpecialOfferOnAll), "SpecialOfferOnAll")]
    [JsonDerivedType(typeof(SpecialOfferOnItems), "SpecialOfferOnItems")]
    internal interface ISpecialOffer
    {
        public SpecialOfferType Type { get; }
        public string Description { get; }
        public bool IsValid(Order order);
        public double ApplyDiscount(Order order);
    }
}
