using Microsoft.Datasync.Client;

namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService;

public class SyncShoppingCart : DatasyncClientData, IEquatable<SyncShoppingCart>
{
    public SyncShoppingCart()
    {
        UserID = string.Empty;
        OrderNumber = string.Empty;
        OrderDate = new DateTime(1900, 1, 1);
        OrderTypeNumber = string.Empty;
        StatusID = 0;
        ConfirmationStatus = 0;
        SentDateTime = new DateTime(1900, 1, 1);
        LabelNumber = string.Empty;
        SeasonNumber = string.Empty;

        JsonMetadataHeader = string.Empty;
        JsonMetadataCustomer = string.Empty;
        JsonMetadataConditions = string.Empty;
    }

    public string UserID { get; set; }
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string OrderTypeNumber { get; set; }
    public int StatusID { get; set; }
    public int ConfirmationStatus { get; set; }
    public DateTime SentDateTime { get; set; }
    public string LabelNumber { get; set; }
    public string SeasonNumber { get; set; }

    public string JsonMetadataHeader { get; set; }
    public string JsonMetadataCustomer { get; set; }
    public string JsonMetadataConditions { get; set; }

    bool IEquatable<SyncShoppingCart>.Equals(SyncShoppingCart? other)
    => other != null && other.Id == Id && other.OrderNumber == OrderNumber && other.OrderDate == OrderDate;

}