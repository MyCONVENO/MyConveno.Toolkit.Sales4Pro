using System;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;
public interface ISyncMetadataHeader
{
    string ConfirmationEmail { get; set; }
    string CustomerOrderNumber { get; set; }
    DateTime DeliveryDateEnd { get; set; }
    DateTime DeliveryDateStart { get; set; }
    string DeliveryDecadeText { get; set; }
    string DeliveryTypeName { get; set; }
    string DeliveryTypeNumber { get; set; }
    bool IsSampleOrder { get; set; }
    bool IsSamplesRequested { get; set; }
    DateTime ModifiedDeliveryDateEnd { get; set; }
    DateTime ModifiedDeliveryDateStart { get; set; }
    byte[] NameCardImage { get; set; }
    DateTime NoDeliveryBefore { get; set; }
    string Remark1 { get; set; }
    string Remark2 { get; set; }
    byte[] SignatureImage { get; set; }
    string TextSnippets { get; set; }
    string ShippingTypeNumber { get; set; }
    string ShippingTypeName { get; set; }
    string Title { get; set; }
}
