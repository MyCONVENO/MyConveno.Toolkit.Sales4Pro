namespace MyConveno.Toolkit.Sales4Pro.Client.Offline.BaseDataService;

public record AssetRecord : BaseModelRecord
{
    public AssetRecord()
    {
        AssetID = string.Empty;
        Metadata = string.Empty;
    }

    public string AssetID { get; init; }
    public string Metadata { get; init; }
}
