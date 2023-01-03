namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;

public interface ISyncShoppingCartItem
{
    string Id { get; set; }
    string ShoppingCartID { get; set; }
    long ShoppingCartItemSort { get; set; }
    string UserID { get; set; }
    string ArticleMetadata { get; set; }
    string ColorMetadata { get; set; }
    string QuantityMetadata { get; set; }

    ISyncMetadataArticle Article { get; set; }
    ISyncMetadataColor Color { get; set; }
    ISyncMetadataQuantity Quantity { get; set; }

    void DeserializeMetadata();
    void SerializeMetadata();
}
