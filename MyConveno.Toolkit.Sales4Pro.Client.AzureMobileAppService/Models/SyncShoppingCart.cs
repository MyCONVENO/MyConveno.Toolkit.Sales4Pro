using Microsoft.Datasync.Client;

namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService;

public class SyncShoppingCart : DatasyncClientData, IEquatable<SyncShoppingCart>
{
    public SyncShoppingCart()
    {
        OrderNumber = string.Empty;
        OrderDate = new DateTime(1900, 1, 1);
        LabelNumber = string.Empty;
        LabelName = string.Empty;
        SeasonNumber = string.Empty;
        SeasonName = string.Empty;
        SeasonLongName = string.Empty;
        OrderTypeNumber = string.Empty;
        OrderTypeName = string.Empty;
        StatusID = 0;
        UserID = string.Empty;
        ConfirmationStatus = 0;
        Sent = false;
        SentDateTime = new DateTime(1900, 1, 1);

        HeaderMetadata = string.Empty;
        AgentMetadata = string.Empty;
        CustomerMetadata = string.Empty;
        AssociationMetadata = string.Empty;
        ConditionsMetadata = string.Empty;
        DeviceMetadata = string.Empty;
    }

    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string LabelNumber { get; set; }
    public string LabelName { get; set; }
    public string SeasonNumber { get; set; }
    public string SeasonName { get; set; }
    public string SeasonLongName { get; set; }
    public string OrderTypeNumber { get; set; }
    public string OrderTypeName { get; set; }
    public int StatusID { get; set; }
    public string UserID { get; set; }
    public int ConfirmationStatus { get; set; }
    public bool Sent { get; set; }
    public DateTime SentDateTime { get; set; }

    public string HeaderMetadata { get; set; }
    public string AgentMetadata { get; set; }
    public string CustomerMetadata { get; set; }
    public string AssociationMetadata { get; set; }
    public string ConditionsMetadata { get; set; }
    public string DeviceMetadata { get; set; }

    bool IEquatable<SyncShoppingCart>.Equals(SyncShoppingCart? other)
    => other != null && other.Id == Id && other.OrderNumber == OrderNumber && other.OrderDate == OrderDate;

}