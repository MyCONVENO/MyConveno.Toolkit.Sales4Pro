using Microsoft.Datasync.Client;
using System;

namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService;

public abstract class SyncShoppingCart : DatasyncClientData, IEquatable<SyncShoppingCart>
{
    public string? OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string? LabelNumber { get; set; }
    public string? LabelName { get; set; }
    public string? SeasonNumber { get; set; }
    public string? SeasonName { get; set; }
    public string? SeasonLongName { get; set; }
    public string? OrderTypeNumber { get; set; }
    public string? OrderTypeName { get; set; }
    public int StatusID { get; set; }
    public string? UserID { get; set; }
    public int ConfirmationStatus { get; set; }
    public bool Sent { get; set; }
    public DateTime SentDateTime { get; set; }

    public string? HeaderMetadata { get; set; }
    public string? AgentMetadata { get; set; }
    public string? CustomerMetadata { get; set; }
    public string? DeliveryAddressMetadata { get; set; }
    public string? InvoiceAddressMetadata { get; set; }
    public string? AssociationMetadata { get; set; }
    public string? ConditionsMetadata { get; set; }
    public string? DeviceMetadata { get; set; }

    bool IEquatable<SyncShoppingCart>.Equals(SyncShoppingCart? other)
    => other != null && other.Id == Id && other.OrderNumber == OrderNumber && other.OrderDate == OrderDate;

}