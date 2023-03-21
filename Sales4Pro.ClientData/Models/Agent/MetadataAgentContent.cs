using CommunityToolkit.Mvvm.ComponentModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public partial class MetadataAgentContent : ObservableObject, IMetadataAgentContent
{
    public MetadataAgentContent()
    {
        Displayname = string.Empty;
        Street = string.Empty;
        ZIP = string.Empty;
        City = string.Empty;
        Country = string.Empty;
        Mobile = string.Empty;
        Phone = string.Empty;
        Email = string.Empty;
        DefaultPricelistNumber = "-";
        IsPriceOnConfirmVisible = true;
        ProcessOrders = false;
    }

    [ObservableProperty]
    public string displayname;
    [ObservableProperty]
    public string street;
    [ObservableProperty]
    public string zIP;
    [ObservableProperty]
    public string city;
    [ObservableProperty]
    public string country;
    [ObservableProperty]
    public string mobile;
    [ObservableProperty]
    public string phone;
    [ObservableProperty]
    public string email;
    [ObservableProperty]
    public string defaultPricelistNumber;
    [ObservableProperty]
    public bool isPriceOnConfirmVisible;
    [ObservableProperty]
    public bool processOrders;
}
