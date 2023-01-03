using System;
using System.Collections.Generic;

namespace Sales4Pro.Common.Metadata
{
    public class MetadataAssetOrderType
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public DateTime DeliveryDateStart { get; set; }
        public DateTime DeliveryDateEnd { get; set; }
        public bool IsDefault { get; set; }
        public List<MetadataAssetCatalog> Catalogs { get; set; }

        public MetadataAssetOrderType()
        {
            Catalogs = new List<MetadataAssetCatalog>();
        }
    }
}