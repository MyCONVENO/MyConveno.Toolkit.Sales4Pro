using Microsoft.AspNetCore.Datasync;
using Microsoft.AspNetCore.Datasync.EFCore;
using Microsoft.AspNetCore.Mvc;

namespace MyConveno.Toolkit.Sales4Pro.Server.SyncDataHost
{
    [Route("tables/synccustomerfavorite")]
    public class SyncCustomerFavoriteController : TableController<SyncCustomerFavorite>
    {
        public SyncCustomerFavoriteController(AppDbContext context) : base(new EntityTableRepository<SyncCustomerFavorite>(context))
        {
            Options = new TableControllerOptions { EnableSoftDelete = true };
        }
    }
}