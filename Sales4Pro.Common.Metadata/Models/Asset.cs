using Newtonsoft.Json;
using Sales4Pro.Common.Metadata.Interfaces;
using SQLite;

namespace Sales4Pro.Common.Metadata.Models
{
    public class Asset : BaseModel, IAsset
    {
        public Asset()
        {
            AssetID = string.Empty;
            Metadata = string.Empty;
            MetadataAsset = new MetadataAsset();
        }

        [PrimaryKey]
        public string AssetID { get; set; }
        public string Metadata { get; set; }
        [Ignore]
        public MetadataAsset MetadataAsset { get; set; }

        public void DeserializeMetadata()
        {
            if (Metadata != null)
                MetadataAsset = JsonConvert.DeserializeObject<MetadataAsset>(Metadata);
        }
    }
}
