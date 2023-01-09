namespace MyConveno.Toolkit.Sales4Pro.Client.Offline.BaseDataService;

public record Customer : BaseModel
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

    public string CustomerID { get; init; }
    public string PricelistID { get; init; }
    public string AgentNumber { get; init; }
    public string CustomerNumber { get; init; }
    public string CustomerName { get; init; }
    public string StartsWithFilter01 { get; init; }
    public string StartsWithFilter02 { get; init; }
    public string StartsWithFilter03 { get; init; }
    public string ContainsFilter01 { get; init; }
    public string ContainsFilter02 { get; init; }
    public string ContainsFilter03 { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public string Metadata { get; init; }
    public string HistoryMetadata { get; init; }
}