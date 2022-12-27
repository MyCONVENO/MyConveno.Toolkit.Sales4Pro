using Newtonsoft.Json;
using Sales4Pro.Common.Metadata.Interfaces;
using System.Collections.Generic;

namespace Sales4Pro.Common.Metadata.Models
{
    public class Customer : BaseModel, ICustomer
    {
        public Customer()
        {
            CustomerID = string.Empty;
            PricelistID = string.Empty;
            AgentNumber = string.Empty;
            CustomerNumber = string.Empty;
            CustomerName = string.Empty;

            MetadataCustomer = new MetadataCustomer();
            MetadataHistories = new List<MetadataCustomerHistory>();
            MetadataAgent = new MetadataAssetAgent();
            MetadataPricelist = new MetadataAssetPricelist();
        }

        public string CustomerID { get; set; }
        public string PricelistID { get; set; }
        public string AgentNumber { get; set; }
        public string CustomerNumber { get; set; }
        public string CustomerName { get; set; }
        public string StartsWithFilter01 { get; set; }
        public string StartsWithFilter02 { get; set; }
        public string StartsWithFilter03 { get; set; }
        public string ContainsFilter01 { get; set; }
        public string ContainsFilter02 { get; set; }
        public string ContainsFilter03 { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Metadata { get; set; }
        public string HistoryMetadata { get; set; }

        public MetadataCustomer MetadataCustomer { get; set; }
        public List<MetadataCustomerHistory> MetadataHistories { get; set; }
        public MetadataAssetAgent MetadataAgent { get; set; }
        public MetadataAssetPricelist MetadataPricelist { get; set; }

        public void DeserializeMetadata()
        {
            MetadataCustomer = JsonConvert.DeserializeObject<MetadataCustomer>(Metadata);
            MetadataHistories = JsonConvert.DeserializeObject<List<MetadataCustomerHistory>>(HistoryMetadata);
        }

    }
}
