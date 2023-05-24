using Microsoft.AspNetCore.Datasync.EFCore;

namespace MyConveno.Toolkit.Sales4Pro.Server.SyncDataHost
{
    public class SyncShoppingCart : EntityTableData
    {
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string Label { get; set; }
        public string Season { get; set; }
        public string OrderType { get; set; }
        public int Status { get; set; }
        public string User { get; set; }
        public string Agent { get; set; }
        public int ConfirmationStatus { get; set; }
        public DateTime SentDateTime { get; set; }

        public string JsonMetadataHeader { get; set; }
        public string JsonMetadataCustomer { get; set; }
        public string JsonMetadataConditions { get; set; }

    }
}