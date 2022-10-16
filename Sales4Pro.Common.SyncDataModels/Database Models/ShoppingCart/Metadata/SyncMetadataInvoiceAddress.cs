using Newtonsoft.Json;
using MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels;

public class SyncMetadataInvoiceAddress : ISyncMetadataInvoiceAddress
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
    public string Mobile { get; set; }
    public string InvoiceNumber { get; set; } // ????

    [JsonIgnore]
    public bool ComputeIsInvoiceAddressEmpty
    {
        get
        {
            return string.IsNullOrEmpty(Name1) &&
                   string.IsNullOrEmpty(Name2) &&
                   string.IsNullOrEmpty(Name3) &&
                   string.IsNullOrEmpty(Street) &&
                   string.IsNullOrEmpty(ZIP) &&
                   string.IsNullOrEmpty(City) &&
                   string.IsNullOrEmpty(CountryName);
        }
    }

    [JsonIgnore]
    public string ComputeZIPCity
    {
        get { return ZIP + " " + City; }
    }

    [JsonIgnore]
    public string ComputeNamesInLine
    {
        get { return (Name1 + " " + Name2 + " " + Name3 + " ").TrimStart().TrimEnd(); }
    }



}
