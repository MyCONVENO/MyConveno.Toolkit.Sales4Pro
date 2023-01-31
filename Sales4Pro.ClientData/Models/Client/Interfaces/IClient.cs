namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public interface IClient
{
    string ClientId { get; set; }
    string ClientName { get; set; }
    string Metadata { get; set; }
    MetadataClientContent MetadataContent { get; set; }

    void DeserializeMetadata();
    void SerializeMetadata();
}