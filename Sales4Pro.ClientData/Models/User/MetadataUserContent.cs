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

        Agents = new List<MetadataAgent>();
        CustomerNumbers = new List<string>();
      
        //Pricelists = new List<MetadataPricelist>();
    }

    public string DisplayName { get; set; }
    public string Role { get; set; }
    public bool IsPriceOnConfirmVisible { get; set; } = true;
    public bool ProcessOrders { get; set; } = false;
    public string DefaultPricelistNumber { get; set; }
    public string Email { get; set; }
    public List<MetadataAgent> Agents { get; set; }
    public List<string> CustomerNumbers { get; set; }
    
    //public List<MetadataPricelist> Pricelists { get; set; }
}
