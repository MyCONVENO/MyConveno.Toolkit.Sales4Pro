using Microsoft.AspNetCore.Datasync;
using Microsoft.AspNetCore.Datasync.EFCore;
using Microsoft.AspNetCore.Mvc;

namespace MyConveno.Toolkit.Sales4Pro.Server.SyncDataHost
{
    [Route("tables/syncshoppingcartitem")]
    public class SyncShoppingCartItemController : TableController<SyncShoppingCartItem>
    {
        public SyncShoppingCartItemController(AppDbContext context) : base(new EntityTableRepository<SyncShoppingCartItem>(context))
        { }
    }
}