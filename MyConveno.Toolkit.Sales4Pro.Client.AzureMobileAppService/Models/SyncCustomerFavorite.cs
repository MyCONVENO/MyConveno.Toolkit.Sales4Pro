using Microsoft.Datasync.Client;
using System;

namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService;

public class SyncCustomerFavorite : DatasyncClientData, IEquatable<SyncCustomerFavorite>
{
    public SyncCustomerFavorite()
    {
        UserID = string.Empty;
        CustomerNumber = string.Empty;
    }

    public string UserID { get; init; }
    public string CustomerNumber { get; init; }

    bool IEquatable<SyncCustomerFavorite>.Equals(SyncCustomerFavorite? other)
     => other != null && other.Id == Id && other.UserID == UserID && other.CustomerNumber == CustomerNumber;

}
