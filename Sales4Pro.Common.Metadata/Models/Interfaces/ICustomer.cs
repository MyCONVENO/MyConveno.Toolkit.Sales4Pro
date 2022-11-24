using System.Collections.Generic;

namespace Sales4Pro.Common.Metadata.Interfaces
{
    public interface ICustomer : IBaseModel
    {
        string CustomerID { get; set; }
        string PricelistID { get; set; }
        string CustomerNumber { get; set; }
        string CustomerName { get; set; }
        string StartsWithFilter01 { get; set; }
        string StartsWithFilter02 { get; set; }
        string StartsWithFilter03 { get; set; }
        string ContainsFilter01 { get; set; }
        string ContainsFilter02 { get; set; }
        string ContainsFilter03 { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }

        MetadataCustomer MetadataCustomer { get; set; }
        List<MetadataCustomerHistory> MetadataHistories { get; set; }
        MetadataAssetAgent MetadataAgent { get; set; }
        MetadataAssetPricelist MetadataPricelist { get; set; }
    }
}
