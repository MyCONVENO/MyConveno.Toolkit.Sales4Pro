using MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels;

public class SyncMetadataDeliveryAddress : ISyncMetadataDeliveryAddress
{
    public string Number { get; set; }
    public string Name1 { get; set; }
    public string Name2 { get; set; }
    public string Name3 { get; set; }
    public string City { get; set; }
    public string ZIP { get; set; }
    public string Street { get; set; }
    public string CountryName { get; set; }
    public string CountryCode { get; set; }
    public string Phone { get; set; }
    public string Fax { get; set; }
    public string EMail { get; set; }
}
