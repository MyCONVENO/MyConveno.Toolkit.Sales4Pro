using MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;
using Newtonsoft.Json;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels;

public class SyncShoppingCartItem : ISyncShoppingCartItem
{
    public SyncShoppingCartItem()
    {
        Article = new SyncMetadataArticle();
        Color = new SyncMetadataColor();
        Quantity = new SyncMetadataQuantity();
    }

    [JsonProperty("id")]
    public string Id { get; set; }
    public string UserID { get; set; }
    public string ShoppingCartID { get; set; }
    public long ShoppingCartItemSort { get; set; }
    public int StatusID { get; set; }

    public string ArticleMetadata { get; set; }
    public string ColorMetadata { get; set; }
    public string QuantityMetadata { get; set; }

    [JsonIgnore]
    public ISyncMetadataArticle Article { get; set; }

    [JsonIgnore]
    public ISyncMetadataColor Color { get; set; }

    [JsonIgnore]
    public ISyncMetadataQuantity Quantity { get; set; }

    public void SerializeMetadata()
    {
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        ArticleMetadata = JsonConvert.SerializeObject(Article, settings);
        ColorMetadata = JsonConvert.SerializeObject(Color, settings);
        QuantityMetadata = JsonConvert.SerializeObject(Quantity, settings);
    }

    public void DeserializeMetadata()
    {
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        if (!string.IsNullOrEmpty(ArticleMetadata.Trim())) // Wichtig!, sonst wird Article auf null gesetzt
            Article = JsonConvert.DeserializeObject<SyncMetadataArticle>(ArticleMetadata, settings);

        if (!string.IsNullOrEmpty(ColorMetadata.Trim())) // Wichtig!, sonst wird Color auf null gesetzt
            Color = JsonConvert.DeserializeObject<SyncMetadataColor>(ColorMetadata, settings);

        if (!string.IsNullOrEmpty(QuantityMetadata.Trim())) // Wichtig!, sonst wird Quantity auf null gesetzt
            Quantity = JsonConvert.DeserializeObject<SyncMetadataQuantity>(QuantityMetadata, settings);
    }

}
