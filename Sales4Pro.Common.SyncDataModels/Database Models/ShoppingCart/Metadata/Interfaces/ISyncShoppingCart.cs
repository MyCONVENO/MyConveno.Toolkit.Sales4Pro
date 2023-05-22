using System;
using System.Collections.Generic;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;

public interface ISyncShoppingCart
{
    string HeaderMetadata { get; set; }
    string CustomerMetadata { get; set; }
    string ConditionsMetadata { get; set; }


    string Id { get; set; }
    string UserID { get; set; }
    string OrderNumber { get; set; }
    int StatusID { get; set; }
    DateTime OrderDate { get; set; }
    string LabelNumber { get; set; }
    string OrderTypeNumber { get; set; }
    string SeasonNumber { get; set; }
    DateTime SentDateTime { get; set; }

    ISyncMetadataHeader Header { get; set; }
    ISyncMetadataCustomer Customer { get; set; }
    ISyncMetadataConditions Conditions { get; set; }
    List<SyncShoppingCartItem> SyncShoppingCartItems { get; set; }

}
