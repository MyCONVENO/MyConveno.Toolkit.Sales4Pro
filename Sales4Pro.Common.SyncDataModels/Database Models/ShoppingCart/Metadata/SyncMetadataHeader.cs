using MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;
using System;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels;

public class SyncMetadataHeader : ISyncMetadataHeader
{
    public string CustomerOrderNumber { get; set; }
    public string Remark1 { get; set; }
    public string Remark2 { get; set; }
    public bool IsSamplesRequested { get; set; }
    public bool IsSampleOrder { get; set; }
    public string ConfirmationEmail { get; set; }
    public string TextSnippets { get; set; }
    public byte[] NameCardImage { get; set; }
    public byte[] SignatureImage { get; set; }
    public string DeliveryTypeNumber { get; set; }
    public string DeliveryTypeName { get; set; }
    public string DeliveryDecadeText { get; set; }
    public DateTime ModifiedDeliveryDateStart { get; set; }
    public DateTime ModifiedDeliveryDateEnd { get; set; }
    public DateTime DeliveryDateStart { get; set; }
    public DateTime DeliveryDateEnd { get; set; }
    public DateTime NoDeliveryBefore { get; set; }
    public string ShippingTypeNumber { get; set; }
    public string ShippingTypeName { get; set; }
    public string Title { get; set; }
}
