using Microsoft.AspNetCore.Datasync;
using Microsoft.AspNetCore.Datasync.EFCore;
using Microsoft.AspNetCore.Mvc;

namespace MyConveno.Toolkit.Sales4Pro.Server.SyncDataHost
{
    [Route("tables/synccustomernote")]
    public class SyncCustomerNoteController : TableController<SyncCustomerNote>
    {
        public SyncCustomerNoteController(AppDbContext context) : base(new EntityTableRepository<SyncCustomerNote>(context))
        {
            Options = new TableControllerOptions { EnableSoftDelete = true };
        }
    }
}