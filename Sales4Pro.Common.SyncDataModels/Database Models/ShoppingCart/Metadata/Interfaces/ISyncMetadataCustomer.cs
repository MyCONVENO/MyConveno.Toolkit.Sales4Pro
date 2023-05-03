namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;

public interface ISyncMetadataCustomer
{
    string Barrier { get; set; }
    string City { get; set; }
    double ComplaintDiscount { get; set; }
    string CountryCode { get; set; }
    string CountryName { get; set; }
    string Customergroup { get; set; }
    string CustomerID { get; set; }
    string CustomerName { get; set; }
    string CustomerNumber { get; set; }
    string DeliveryTypeName { get; set; }
    string DeliveryTypeNumber { get; set; }
    double Discount { get; set; }
    double DistanceToCurrentLocation { get; set; }
    string EMail { get; set; }
    string Fax { get; set; }
    double Lat { get; set; }
    double Lon { get; set; }
    string Mobile { get; set; }
    string Name1 { get; set; }
    string Name2 { get; set; }
    string Name3 { get; set; }
    double OpenReceivables { get; set; }
    string PaymentMethodNumber { get; set; }
    string PaymentMethodText { get; set; }
    string PaymentTerm { get; set; }
    string PaymentTermNumber { get; set; }
    string PaymentTermText { get; set; }
    string Phone { get; set; }
    string PricelistNumber { get; set; }
    string Street { get; set; }
    string TaxID { get; set; }
    int Valuta { get; set; }
    string ZIP { get; set; }
    string ShippingTypeName { get; set; }
    string ShippingTypeNumber { get; set; }
    
    bool HasInvoiceAddress { get; set; }
     string InvoiceAddressNumber { get; set; }
     string InvoiceAddressName { get; set; }
     string InvoiceAddressCity { get; set; }
     string InvoiceAddressZIP { get; set; }
     string InvoiceAddressStreet { get; set; }
     string InvoiceAddressCountryName { get; set; }
     string InvoiceAddressCountryCode { get; set; }
     string InvoiceAddressPhone { get; set; }
     string InvoiceAddressEMail { get; set; }

     bool HasDeliveryAddress { get; set; }
     string DeliveryAddressNumber { get; set; }
     string DeliveryAddressName { get; set; }
     string DeliveryAddressCity { get; set; }
     string DeliveryAddressZIP { get; set; }
     string DeliveryAddressStreet { get; set; }
     string DeliveryAddressCountryName { get; set; }
     string DeliveryAddressCountryCode { get; set; }
     string DeliveryAddressPhone { get; set; }
     string DeliveryAddressEMail { get; set; }

}