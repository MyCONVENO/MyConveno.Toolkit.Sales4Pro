using System.Collections.ObjectModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public class MetadataAgentContent : IMetadataAgentContent
{
    public MetadataAgentContent()
    {
        DisplayName = string.Empty;
        Mobile = string.Empty;
        Phone = string.Empty;
        Email = string.Empty;
        ConfirmationEmail = string.Empty;
        DefaultPricelistNumber = string.Empty;
        Pricelists = new ObservableCollection<Pricelist>();
        IsDeletable = true;
    }

    public string DisplayName { get; set; }
    public string Mobile { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string ConfirmationEmail { get; set; }
    public string DefaultPricelistNumber { get; set; }
    public ObservableCollection<Pricelist> Pricelists { get; set; }
    public bool IsDeletable { get; set; }

}