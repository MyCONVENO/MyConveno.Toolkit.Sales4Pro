using Microsoft.AspNetCore.Datasync.EFCore;

namespace MyConveno.Toolkit.Sales4Pro.Server.SyncDataHost
{
    public class SyncShoppingCart : EntityTableData
    {
        public string User { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int Status { get; set; }
        public int ConfirmationStatus { get; set; }
        public DateTime SentDateTime { get; set; }
        public string JsonMetadata { get; set; }
    
    }
}