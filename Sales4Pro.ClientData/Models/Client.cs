using MyConveno.Toolkit.Sales4Pro.Common.ClientData.Interfaces;
using Newtonsoft.Json;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public class Client : IClient
{
    public Client()
    {
        MetadataContent = new MetadataClientContent();
    }

    public string ClientId { get; set; }
    public string ClientName { get; set; }
    public string Metadata { get; set; }

    [JsonIgnore]
    public IMetadataClientContent MetadataContent { get; set; }

    public void SerializeMetadata()
    {
        Metadata = JsonConvert.SerializeObject((MetadataClientContent)MetadataContent);
    }

    public void DeserializeMetadata()
    {
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        if (!string.IsNullOrEmpty(Metadata.Trim())) // Wichtig!, sonst wird Content auf null gesetzt
            MetadataContent = JsonConvert.DeserializeObject<MetadataClientContent>(Metadata, settings);
    }

}
