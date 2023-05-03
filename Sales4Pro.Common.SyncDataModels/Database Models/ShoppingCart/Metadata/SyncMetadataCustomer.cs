using MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels;

public class SyncMetadataCustomer : ISyncMetadataCustomer
{
    public string CustomerID { get; set; }
    public string CustomerNumber { get; set; }
    public string CustomerName { get; set; }
    public string PricelistNumber { get; set; }
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
    public string Customergroup { get; set; }
    public string PaymentTerm { get; set; }
    public double Discount { get; set; }
    public string TaxID { get; set; }
    public double ComplaintDiscount { get; set; }
    public int Valuta { get; set; }
    public string PaymentMethodText { get; set; }
    public string PaymentMethodNumber { get; set; }
    public string PaymentTermText { get; set; }
    public string PaymentTermNumber { get; set; }
    public string Barrier { get; set; }
    public string DeliveryTypeNumber { get; set; }
    public string DeliveryTypeName { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public double DistanceToCurrentLocation { get; set; }
    public double OpenReceivables { get; set; }
   
    public string ShippingTypeName { get; set; }
    public string ShippingTypeNumber { get; set; }

    public bool HasInvoiceAddress { get; set; }
    public string InvoiceAddressNumber { get; set; }
    public string InvoiceAddressName { get; set; }
     public string InvoiceAddressCity { get; set; }
    public string InvoiceAddressZIP { get; set; }
    public string InvoiceAddressStreet { get; set; }
    public string InvoiceAddressCountryName { get; set; }
    public string InvoiceAddressCountryCode { get; set; }
    public string InvoiceAddressPhone { get; set; }
    public string InvoiceAddressEMail { get; set; }

    public bool HasDeliveryAddress { get; set; }
    public string DeliveryAddressNumber { get; set; }
    public string DeliveryAddressName { get; set; }
    public string DeliveryAddressCity { get; set; }
    public string DeliveryAddressZIP { get; set; }
    public string DeliveryAddressStreet { get; set; }
    public string DeliveryAddressCountryName { get; set; }
    public string DeliveryAddressCountryCode { get; set; }
    public string DeliveryAddressPhone { get; set; }
    public string DeliveryAddressEMail { get; set; }

}
