using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public class SpecialDiscount : ISpecialDiscount
{
    public SpecialDiscount()
    {
        Metadata = string.Empty;
        MetadataContent = new MetadataSpecialDiscountContent();
    }

    public string SpecialDiscountId { get; set; }
    public string Metadata { get; set; }

    [JsonIgnore]
    public MetadataSpecialDiscountContent MetadataContent { get; set; }

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
            MetadataContent = JsonSerializer.Deserialize<MetadataSpecialDiscountContent>(Metadata, settings);
    }
}
