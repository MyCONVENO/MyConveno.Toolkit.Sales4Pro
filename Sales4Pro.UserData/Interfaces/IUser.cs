namespace MyConveno.Toolkit.Sales4Pro.Client.UserData;

public interface IUser
{
    string UserId { get; set; }
    string UserName { get; set; }
    string Password { get; set; }
    string Metadata { get; set; }
    MetadataUserContent MetadataContent { get; set; }

    void DeserializeMetadata();
    void SerializeMetadata();
}
