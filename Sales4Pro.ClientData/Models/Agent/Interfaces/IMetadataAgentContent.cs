using System.Collections.ObjectModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public interface IMetadataAgentContent
{
    string DisplayName { get; set; }
    string Phone { get; set; }
    string Mobile { get; set; }
    string Email { get; set; }
    string ConfirmationEmail { get; set; }
    string DefaultPricelistNumber { get; set; }
    ObservableCollection<Pricelist> Pricelists { get; set; }
    bool IsDeletable { get; set; }

}