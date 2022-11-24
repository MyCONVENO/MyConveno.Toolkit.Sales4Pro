using System.Collections.Generic;

namespace Sales4Pro.Common.Metadata
{
    public class MetadataAssetSeason
    {
        public string Name { get; set; }
        public string Number { get; set; }
        //public int Sort { get; set; }
        //public DateTime? DeliveryDateStart { get; set; }
        //public DateTime? DeliveryDateEnd { get; set; }
        //public DateTime? SeasonDeliveryDateStart { get; set; }
        //public DateTime? SeasonDeliveryDateEnd { get; set; }
        public bool IsDefault { get; set; }

        public List<MetadataAssetOrderType> OrderTypes { get; set; }

        public MetadataAssetSeason()
        {
            OrderTypes = new List<MetadataAssetOrderType>();
        }

    }
}