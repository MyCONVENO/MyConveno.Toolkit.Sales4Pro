using Newtonsoft.Json;
using Sales4Pro.Common.Metadata.Interfaces;
using SQLite;
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

        [PrimaryKey, Indexed]
        public string CustomerID { get; set; }

        [Indexed]
        public string PricelistID { get; set; }

        [Indexed]
        public string AgentNumber { get; set; }

        [Indexed]
        public string CustomerNumber { get; set; }

        [Indexed]
        public string CustomerName { get; set; }

        [Indexed]
        public string StartsWithFilter01 { get; set; }

        [Indexed]
        public string StartsWithFilter02 { get; set; }

        [Indexed]
        public string StartsWithFilter03 { get; set; }

        [Indexed]
        public string ContainsFilter01 { get; set; }

        [Indexed]
        public string ContainsFilter02 { get; set; }

        [Indexed]
        public string ContainsFilter03 { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Metadata { get; set; }

        public string HistoryMetadata { get; set; }

        [Ignore]
        public MetadataCustomer MetadataCustomer { get; set; }

        [Ignore]
        public List<MetadataCustomerHistory> MetadataHistories { get; set; }

        [Ignore]
        public MetadataAssetAgent MetadataAgent { get; set; }

        [Ignore]
        public MetadataAssetPricelist MetadataPricelist { get; set; }


        public void DeserializeMetadata()
        {
            MetadataCustomer = JsonConvert.DeserializeObject<MetadataCustomer>(Metadata);
            MetadataHistories = JsonConvert.DeserializeObject<List<MetadataCustomerHistory>>(HistoryMetadata);
        }

    }
}
