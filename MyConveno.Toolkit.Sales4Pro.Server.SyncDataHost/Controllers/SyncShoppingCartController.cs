using Microsoft.AspNetCore.Datasync;
using Microsoft.AspNetCore.Datasync.EFCore;
using Microsoft.AspNetCore.Mvc;

namespace MyConveno.Toolkit.Sales4Pro.Server.SyncDataHost
{
    [Route("tables/syncshoppingcart")]
    public class SyncShoppingCartController : TableController<SyncShoppingCart>
    {
        public SyncShoppingCartController(AppDbContext context) : base(new EntityTableRepository<SyncShoppingCart>(context))
        { }
    }
}