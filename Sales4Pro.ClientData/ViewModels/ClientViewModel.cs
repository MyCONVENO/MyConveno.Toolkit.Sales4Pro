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
    public MetadataClientContent clientContent = new();

    #endregion

    #region Computed Properties

    public string ComputeZIPCity
    {
        get
        {
            return string.Format("{0} {1}", ClientContent.Reports.AddressZip, ClientContent.Reports.AddressCity);
        }
    }

    #endregion

    public void PasteData(Client client)
    {
        if (client == null)
        {
            ClientId = string.Empty;
            ClientName = string.Empty;

            ClientContent = new MetadataClientContent();
        }
        else
        {
            ClientId = client.ClientId;
            ClientName = client.ClientName;

            Client tempClient = new() { Metadata = client.Metadata };
            tempClient.DeserializeMetadata();
            ClientContent = tempClient.MetadataContent;
        }
    }

    public Client GetModel()
    {
        Client model = new()
        {
            ClientId = ClientId,
            ClientName = ClientName,

            MetadataContent = ClientContent
        };
        model.SerializeMetadata();
        return model;
    }

}
