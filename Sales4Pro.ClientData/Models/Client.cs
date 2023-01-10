using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public class Client 
{
    public Client()
    {
        MetadataContent = new MetadataClientContent();
    }

    public string ClientId { get; set; }
    public string ClientName { get; set; }
    public string Metadata { get; set; }

    [JsonIgnore]
    public MetadataClientContent MetadataContent { get; set; }

    public void SerializeMetadata()
    {
        Metadata = JsonSerializer.Serialize(MetadataContent);
    }

    public void DeserializeMetadata()
    {
        JsonSerializerOptions settings = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        if (!string.IsNullOrEmpty(Metadata.Trim())) // Wichtig!, sonst wird Content auf null gesetzt
            MetadataContent = JsonSerializer.Deserialize<MetadataClientContent>(Metadata, settings);
    }

}
