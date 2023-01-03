using System;
using System.Collections.Generic;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;

public interface ISyncShoppingCart
{
    string AgentMetadata { get; set; }
    string AssociationMetadata { get; set; }
    string ConditionsMetadata { get; set; }
    int ConfirmationStatus { get; set; }
    string CustomerMetadata { get; set; }
    string DeliveryAddressMetadata { get; set; }
    string DeviceMetadata { get; set; }
    string HeaderMetadata { get; set; }


    string Id { get; set; }
    string InvoiceAddressMetadata { get; set; }
    string LabelName { get; set; }
    string LabelNumber { get; set; }
    DateTime OrderDate { get; set; }
    string OrderNumber { get; set; }
    string OrderTypeName { get; set; }
    string OrderTypeNumber { get; set; }
    string SeasonLongName { get; set; }
    string SeasonName { get; set; }
    string SeasonNumber { get; set; }
    bool Sent { get; set; }
    DateTime SentDateTime { get; set; }
    int StatusID { get; set; }
    string UserID { get; set; }

    ISyncMetadataHeader Header { get; set; }
    ISyncMetadataAgent Agent { get; set; }
    ISyncMetadataCustomer Customer { get; set; }
    ISyncMetadataDeliveryAddress DeliveryAddress { get; set; }
    ISyncMetadataInvoiceAddress InvoiceAddress { get; set; }
    ISyncMetadataAssociation Association { get; set; }
    ISyncMetadataConditions Conditions { get; set; }
    ISyncMetadataDevice Device { get; set; }
    List<SyncShoppingCartItem> SyncShoppingCartItems { get; set; }

}
