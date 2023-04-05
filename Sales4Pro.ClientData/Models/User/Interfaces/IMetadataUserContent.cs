namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public interface IMetadataUserContent
{
    string DisplayName { get; set; }
    string Role { get; set; }
    bool IsPriceOnConfirmVisible { get; set; }
    bool ProcessOrders { get; set; }
    string DefaultPricelistNumber { get; set; }
    string Email { get; set; }
    List<MetadataAgent> Agents { get; set; }
    List<string> CustomerNumbers { get; set; }

    // List<MetadataPricelist> Pricelists { get; set; }

}
