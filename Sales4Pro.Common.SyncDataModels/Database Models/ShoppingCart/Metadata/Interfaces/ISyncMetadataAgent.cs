namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;

public interface ISyncMetadataAgent
{
    string Name { get; }
    string Number { get; }
    string City { get; }
    string CollectionFilter { get; }
    decimal Commission { get; }
    string ConfirmEmail { get; }
    string ConfirmFax { get; }
    string CountryCode { get; }
    string CountryName { get; }
    string DefaultPaymentTermText { get; }
    string EMail { get; }
    string Fax { get; }
    string Mobile { get; }
    string Name1 { get; }
    string Name2 { get; }
    string Phone { get; }
    string PricelistID { get; }
    string Remark { get; }
    string Street { get; }
    string ZIP { get; }

}