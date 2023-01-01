using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyConveno.Toolkit.Sales4Pro.Client.UserData;

public class User : IUser
{
    public User()
    {
        MetadataContent = new MetadataUserContent();
    }

    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Metadata { get; set; }

    [JsonIgnore]
    public MetadataUserContent MetadataContent { get; set; }

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
            MetadataContent = JsonSerializer.Deserialize<MetadataUserContent>(Metadata, settings);
    }
}
