using Microsoft.Datasync.Client;

namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService;

public class SyncShoppingCart : DatasyncClientData, IEquatable<SyncShoppingCart>
{
    public SyncShoppingCart()
    {
        User = string.Empty;
        OrderNumber = string.Empty;
        OrderDate = new DateTime(1900, 1, 1);
        Status = 0;
        ConfirmationStatus = 0;
        SentDateTime = new DateTime(1900, 1, 1);
        JsonMetadata = string.Empty;
    }

    public string User { get; set; }
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public int Status { get; set; }
    public int ConfirmationStatus { get; set; }
    public DateTime SentDateTime { get; set; }
    public string JsonMetadata { get; set; }

    bool IEquatable<SyncShoppingCart>.Equals(SyncShoppingCart? other)
    => other != null && other.Id == Id && other.OrderNumber == OrderNumber && other.OrderDate == OrderDate;

}