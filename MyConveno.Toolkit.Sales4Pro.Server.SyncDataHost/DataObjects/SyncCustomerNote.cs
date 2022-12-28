using Microsoft.AspNetCore.Datasync.EFCore;

namespace MyConveno.Toolkit.Sales4Pro.Server.SyncDataHost
{
    public class SyncCustomerNote : EntityTableData
    {
        public string CustomerNumber { get; set; }
        public string NoteText { get; set; }
        public byte[] NoteImage { get; set; }
        public DateTimeOffset NoteCreated { get; set; }
    }
}