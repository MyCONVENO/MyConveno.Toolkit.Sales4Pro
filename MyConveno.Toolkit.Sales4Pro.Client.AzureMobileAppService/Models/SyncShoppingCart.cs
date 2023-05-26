using Microsoft.Datasync.Client;

namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService;

public class SyncShoppingCart : DatasyncClientData, IEquatable<SyncShoppingCart>
{
    public SyncShoppingCart()
    {
        OrderNumber = string.Empty;
        OrderDate = new DateTime(1900, 1, 1);
        Label = string.Empty;
        Season = string.Empty;
        OrderType = string.Empty;
        Status = 0;
        User = string.Empty;
        Agent = string.Empty;
        ConfirmationStatus = 0;
        SentDateTime = new DateTime(1900, 1, 1);
        JsonMetadata = string.Empty;
    }

    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string Label { get; set; }
    public string Season { get; set; }
    public string OrderType { get; set; }
    public int Status { get; set; }
    public string User { get; set; }
    public string Agent { get; set; }
    public int ConfirmationStatus { get; set; }
    public DateTime SentDateTime { get; set; }
    public string JsonMetadata { get; set; }

    bool IEquatable<SyncShoppingCart>.Equals(SyncShoppingCart? other)
    => other != null && other.Id == Id && other.OrderNumber == OrderNumber && other.OrderDate == OrderDate;

}