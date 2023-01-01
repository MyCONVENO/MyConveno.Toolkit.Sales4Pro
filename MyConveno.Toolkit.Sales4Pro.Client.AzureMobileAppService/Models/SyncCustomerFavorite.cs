using Microsoft.Datasync.Client;
using System;

namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService;

abstract class SyncCustomerFavorite : DatasyncClientData, IEquatable<SyncCustomerFavorite>
{
    public string? UserID { get; set; }
    public string? CustomerNumber { get; set; }

    bool IEquatable<SyncCustomerFavorite>.Equals(SyncCustomerFavorite? other)
     => other != null && other.Id == Id && other.UserID == UserID && other.CustomerNumber == CustomerNumber;
}
