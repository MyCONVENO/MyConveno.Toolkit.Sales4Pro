using Microsoft.AspNetCore.Datasync.EFCore;
using System.ComponentModel.DataAnnotations;

namespace MyConveno.Toolkit.Sales4Pro.Server.SyncDataHost
{
    public class SyncShoppingCartItem : EntityTableData
    {
        public string UserName { get; set; }

        [Required, MinLength(1)]
        public string ShoppingCartID { get; set; }
        public long ShoppingCartItemSort { get; set; }

        public string ArticleMetadata { get; set; }
        public string ColorMetadata { get; set; }
        public string QuantityMetadata { get; set; }
    }
}