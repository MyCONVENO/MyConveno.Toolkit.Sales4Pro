using MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels;

public class SyncMetadataAgent : ISyncMetadataAgent
{
    public string Number { get; set; }
    public string Name { get; set; }
    public string Name1 { get; set; }
    public string Name2 { get; set; }
    public string Street { get; set; }
    public string ZIP { get; set; }
    public string City { get; set; }
    public string CountryCode { get; set; }
    public string CountryName { get; set; }
    public string Phone { get; set; }
    public string Fax { get; set; }
    public string EMail { get; set; }
    public string Mobile { get; set; }
    public string ConfirmFax { get; set; }
    public string ConfirmEmail { get; set; }
    public string Remark { get; set; }
    public string PricelistID { get; set; }
    public decimal Commission { get; set; }
    public string CollectionFilter { get; set; }
    public string DefaultPaymentTermText { get; set; }
}
