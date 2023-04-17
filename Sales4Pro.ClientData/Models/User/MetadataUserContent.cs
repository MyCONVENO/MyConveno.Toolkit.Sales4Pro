using System.Collections.ObjectModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public class MetadataUserContent : IMetadataUserContent
{
    public MetadataUserContent()
    {
        Role = "User";
        DefaultPricelistNumber = "100";
        DisplayName = string.Empty;
        IsPriceOnConfirmVisible = true;
        ProcessOrders = false;
        Email = string.Empty;

        Agents = new ObservableCollection<MetadataAgent>();
        Pricelists = new ObservableCollection<MetadataPricelist>();
        CustomerNumbers = new ObservableCollection<string>();
    }

    public string DisplayName { get; set; }
    public string Role { get; set; }
    public bool IsPriceOnConfirmVisible { get; set; }
    public bool ProcessOrders { get; set; } = false;
    public string DefaultPricelistNumber { get; set; }
    public string Email { get; set; }
    public ObservableCollection<MetadataAgent> Agents { get; set; }
    public ObservableCollection<MetadataPricelist> Pricelists { get; set; }
    public ObservableCollection<string> CustomerNumbers { get; set; }
   
}
