using System.Collections.Generic;

namespace Sales4Pro.Common.Metadata
{
    public class MetadataAsset
    {
        public List<MetadataAssetSeason> Seasons { get; set; }
        //public List<MetadataAssetLabel> Catalogs { get; set; }
        public List<MetadataAssetOrderType> OrderTypes { get; set; }
        public List<MetadataAssetPricelist> Pricelists { get; set; }
        public List<MetadataAssetCountry> Countries { get; set; }
        public List<MetadataAssetAgent> Agents { get; set; }
        public List<MetadataAssetDeliveryType> DeliveryTypes { get; set; }
        public List<MetadataAssetPaymentMethod> PaymentMethods { get; set; }
        public List<MetadataAssetPaymentTerm> PaymentTerms { get; set; }

        public MetadataAsset()
        {
            Seasons = new List<MetadataAssetSeason>();
            //Catalogs = new List<MetadataAssetLabel>();
            OrderTypes = new List<MetadataAssetOrderType>();
            Countries = new List<MetadataAssetCountry>();
            Agents = new List<MetadataAssetAgent>();
            DeliveryTypes = new List<MetadataAssetDeliveryType>();
            PaymentMethods = new List<MetadataAssetPaymentMethod>();
            PaymentTerms = new List<MetadataAssetPaymentTerm>();
        }

    }
}