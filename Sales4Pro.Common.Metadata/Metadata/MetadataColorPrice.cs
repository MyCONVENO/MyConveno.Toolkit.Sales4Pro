namespace Sales4Pro.Common.Metadata
{
    public class MetadataColorPrice
    {
        public string PricelistNumber { get; set; }
        public double BuyingPrice { get; set; }
        public string BuyingPriceCurrency { get; set; }
        public double SalesPrice { get; set; }
        public string SalesPriceCurrency { get; set; }

        public string ComputeBuyingPriceWithCurrency
        {
            get { return BuyingPrice.ToString("N2") + " " + (BuyingPriceCurrency ?? ""); }
        }

        public string ComputeSalesPriceWithCurrency
        {
            get { return SalesPrice.ToString("N2") + " " + (SalesPriceCurrency ?? ""); }
        }

    }
}