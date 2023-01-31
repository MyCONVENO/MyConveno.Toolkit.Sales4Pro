using CommunityToolkit.Mvvm.ComponentModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public partial class ClientViewModel : ObservableObject
{
    public override string ToString()
    {
        return ClientName;
    }

    #region Observable Properties

    [ObservableProperty]
    public string clientId = string.Empty;

    [ObservableProperty]
    public string clientName = string.Empty;

    [ObservableProperty]
    public MetadataClientContent metadataClientContent = new();

    #endregion

    #region Computed Properties

    #endregion

    public void PasteData(Client client)
    {
        if (client == null)
        {
            ClientId = string.Empty;
            ClientName = string.Empty;

            MetadataClientContent = new MetadataClientContent();
        }
        else
        {
            ClientId = client.ClientId;
            ClientName = client.ClientName;

            Client tempClient = new() { Metadata = client.Metadata };
            tempClient.DeserializeMetadata();
            MetadataClientContent = tempClient.MetadataContent;
        }
    }

    public Client GetModel()
    {
        Client model = new()
        {
            ClientId = ClientId,
            ClientName = ClientName,

            MetadataContent = MetadataClientContent
        };
        model.SerializeMetadata();
        return model;
    }

}
