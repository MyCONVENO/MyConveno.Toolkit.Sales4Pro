using Microsoft.Datasync.Client;
using System.ComponentModel.DataAnnotations;
using System;

namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService;

public abstract class SyncShoppingCartItem : DatasyncClientData, IEquatable<SyncShoppingCartItem>
{
    public string? UserID { get; set; }

    [Required, MinLength(1)]
    public string? ShoppingCartID { get; set; }
    public long ShoppingCartItemSort { get; set; }

    public string? ArticleMetadata { get; set; }
    public string? ColorMetadata { get; set; }
    public string? QuantityMetadata { get; set; }


    bool IEquatable<SyncShoppingCartItem>.Equals(SyncShoppingCartItem? other)
    => other != null && other.Id == Id && other.UserID == UserID && other.ShoppingCartID == ShoppingCartID;

}