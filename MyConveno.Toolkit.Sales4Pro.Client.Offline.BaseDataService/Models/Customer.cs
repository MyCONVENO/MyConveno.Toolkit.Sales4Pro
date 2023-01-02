namespace MyConveno.Toolkit.Sales4Pro.Client.Offline.BaseDataService;

public class Customer
{
    public Customer()
    {
        CustomerID = string.Empty;
        PricelistID = string.Empty;
        AgentNumber = string.Empty;
        CustomerNumber = string.Empty;
        CustomerName = string.Empty;

        StartsWithFilter01 = string.Empty;
        StartsWithFilter02 = string.Empty;
        StartsWithFilter03 = string.Empty;
        ContainsFilter01 = string.Empty;
        ContainsFilter02 = string.Empty;
        ContainsFilter03 = string.Empty;
        Latitude = 0;
        Longitude = 0;
        Metadata = string.Empty;
        HistoryMetadata = string.Empty;
    }

    public string CustomerID { get; set; }
    public string PricelistID { get; set; }
    public string AgentNumber { get; set; }
    public string CustomerNumber { get; set; }
    public string CustomerName { get; set; }
    public string StartsWithFilter01 { get; set; }
    public string StartsWithFilter02 { get; set; }
    public string StartsWithFilter03 { get; set; }
    public string ContainsFilter01 { get; set; }
    public string ContainsFilter02 { get; set; }
    public string ContainsFilter03 { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Metadata { get; set; }
    public string HistoryMetadata { get; set; }
}