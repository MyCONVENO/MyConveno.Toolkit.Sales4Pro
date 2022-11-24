using Newtonsoft.Json;
using Sales4Pro.Common.Metadata.Interfaces;
using Sales4Pro.Common.Metadata.Models;
using System.Collections.Generic;

namespace Sales4Pro.Common.Metadata;

public class CustomerModel : BaseModel, ICustomer
{
    public string CustomerID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerNumber { get; set; }
    public string StartsWithFilter01 { get; set; }
    public string StartsWithFilter02 { get; set; }
    public string StartsWithFilter03 { get; set; }
    public string ContainsFilter01 { get; set; }
    public string ContainsFilter02 { get; set; }
    public string ContainsFilter03 { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string CustomerMetadata { get; set; }
    public string HistoryMetadata { get; set; }
    public string AgentNumber { get; set; }
    public string AgentName { get; set; }
    public string AgentMetadata { get; set; }
    public string PricelistID { get; set; }
    public MetadataCustomer MetadataCustomer { get; set; }
    public List<MetadataCustomerHistory> MetadataHistories { get; set; }
    public MetadataAssetAgent MetadataAgent { get; set; }
    public MetadataAssetPricelist MetadataPricelist { get; set; }
    public MetadataAssetDeliveryType MetadataDeliveryType { get; set; }
    public MetadataColorPrice MetadataPrice { get; set; }

    public CustomerModel()
    {
        MetadataCustomer = new MetadataCustomer();
        MetadataHistories = new List<MetadataCustomerHistory>();
        MetadataAgent = new MetadataAssetAgent();
        MetadataPricelist = new MetadataAssetPricelist();
        MetadataPrice = new MetadataColorPrice();
        MetadataDeliveryType = new MetadataAssetDeliveryType();
    }

    public void DeserializeMetadata()
    {
        JsonSerializerSettings serSettings = new JsonSerializerSettings();
        serSettings.NullValueHandling = NullValueHandling.Ignore;

        MetadataCustomer = JsonConvert.DeserializeObject<MetadataCustomer>(CustomerMetadata, serSettings);
        MetadataHistories = JsonConvert.DeserializeObject<List<MetadataCustomerHistory>>(HistoryMetadata, serSettings);
    }
}

