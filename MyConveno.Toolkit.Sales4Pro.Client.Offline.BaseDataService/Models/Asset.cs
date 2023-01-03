namespace MyConveno.Toolkit.Sales4Pro.Client.Offline.BaseDataService;

public class Asset
{
    public Asset()
    {
        AssetID = string.Empty;
        Metadata = string.Empty;
    }

    public string AssetID { get; set; }
    public string Metadata { get; set; }
}
