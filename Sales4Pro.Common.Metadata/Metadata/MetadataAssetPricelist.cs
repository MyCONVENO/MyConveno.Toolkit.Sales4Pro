using Newtonsoft.Json;

namespace Sales4Pro.Common.Metadata
{
    public class MetadataAssetPricelist
    {
        public MetadataAssetPricelist()
        {

        }

        public MetadataAssetPricelist(string pricelistNumber, string pricelistName, string buyingCurrency, string salesCurrency, bool isdefault)
        {
            Number = pricelistNumber;
            Name = pricelistName;
            BuyingCurrency = buyingCurrency;
            SalesCurrency = salesCurrency;
            IsDefault = isdefault;
        }

        [JsonIgnore]
        public string ComputeLongName
        {
            get
            {
                return (string.Format("{0} ({1}, {2})", Number, BuyingCurrency, SalesCurrency));
            }
        }

        public override string ToString()
        {
            return Number;
        }

        public string Number { get; set; }
        public string Name { get; set; }
        public string BuyingCurrency { get; set; }
        public string SalesCurrency { get; set; }
        public bool IsDefault { get; set; }
    }
}