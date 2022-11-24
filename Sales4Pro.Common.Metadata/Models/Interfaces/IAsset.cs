namespace Sales4Pro.Common.Metadata.Interfaces
{
    public interface IAsset : IBaseModel
    {
        string AssetID { get; set; }
        string Metadata { get; set; }
        MetadataAsset MetadataAsset { get; set; }
    }
}
