using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyConveno.Toolkit.Sales4Pro.Client.UserData;

public class Agent : IAgent
{
    public Agent()
    {
        MetadataContent = new MetadataAgentContent();
    }

    public string AgentNumber { get; set; }
    public string AgentName { get; set; }
    public string Metadata { get; set; }

    [JsonIgnore]
    public MetadataAgentContent MetadataContent { get; set; }

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
            MetadataContent = JsonSerializer.Deserialize<MetadataAgentContent>(Metadata, settings);
    }
}
