using Microsoft.Datasync.Client;

namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService;

public class SyncCustomerFavorite : DatasyncClientData, IEquatable<SyncCustomerFavorite>
{
    public SyncCustomerFavorite()
    {
        UserName = string.Empty;
        CustomerNumber = string.Empty;
    }

    public string UserName { get; init; }
    public string CustomerNumber { get; init; }

    bool IEquatable<SyncCustomerFavorite>.Equals(SyncCustomerFavorite? other)
     => other != null && other.Id == Id && other.UserName == UserName && other.CustomerNumber == CustomerNumber;

}
