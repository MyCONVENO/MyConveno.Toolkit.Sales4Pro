namespace MyConveno.Toolkit.Sales4Pro.Common.ClientData.Interfaces;

public interface IClient
{
    string ClientId { get; set; }
    string ClientName { get; set; }
    string Metadata { get; set; }
    IMetadataClientContent MetadataContent { get; set; }

    void DeserializeMetadata();
    void SerializeMetadata();
}