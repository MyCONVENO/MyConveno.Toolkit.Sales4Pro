using System.Collections.ObjectModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public interface IMetadataUserContent
{
    string DisplayName { get; set; }
    string Role { get; set; }
    bool IsPriceOnConfirmVisible { get; set; }
    bool ProcessOrders { get; set; }
    string DefaultAgentNumber { get; set; }
    string Email { get; set; }
    ObservableCollection<Agent> Agents { get; set; }
    ObservableCollection<string> CustomerNumbers { get; set; }
}
