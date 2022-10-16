using MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels;

public class SyncCustomerFavorite : ISyncCustomerFavorite
{
    public SyncCustomerFavorite()
    {
        UserID = string.Empty;
        CustomerNumber = string.Empty;
    }

    [Newtonsoft.Json.JsonProperty("id")]
    public string Id { get; set; }

    [Newtonsoft.Json.JsonProperty("userID")]
    public string UserID { get; set; }

    [Newtonsoft.Json.JsonProperty("customerNumber")]
    public string CustomerNumber { get; set; }

}
