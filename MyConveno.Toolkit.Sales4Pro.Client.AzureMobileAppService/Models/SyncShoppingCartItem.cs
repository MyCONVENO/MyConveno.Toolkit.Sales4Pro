using Microsoft.Datasync.Client;
using System.ComponentModel.DataAnnotations;

namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService;

public class SyncShoppingCartItem : DatasyncClientData, IEquatable<SyncShoppingCartItem>
{
    public SyncShoppingCartItem()
    {
        UserName = string.Empty;
        ShoppingCartID = string.Empty;
        ShoppingCartItemSort = 0L;
        ArticleMetadata = string.Empty;
        ColorMetadata = string.Empty;
        QuantityMetadata = string.Empty;
    }

    public string UserName { get; set; }

    [Required, MinLength(1)]
    public string ShoppingCartID { get; set; }
    public long ShoppingCartItemSort { get; set; }

    public string ArticleMetadata { get; set; }
    public string ColorMetadata { get; set; }
    public string QuantityMetadata { get; set; }

    bool IEquatable<SyncShoppingCartItem>.Equals(SyncShoppingCartItem? other)
    => other != null && other.Id == Id && other.UserName == UserName && other.ShoppingCartID == ShoppingCartID;

}