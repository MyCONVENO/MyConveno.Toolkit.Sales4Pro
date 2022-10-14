using System.Collections.Generic;

namespace MyConveno.Toolkit.Sales4Pro.Client.UserData;

public class MetadataUserContent : IMetadataUserContent
{
    public MetadataUserContent()
    {
        Role = "User";
        DefaultPricelistNumber = "100";
        Displayname = string.Empty;
        IsPriceOnConfirmVisible = true;
        ProcessOrders = false;
        Email = string.Empty;
        Mobile = string.Empty;
        Phone = string.Empty;
        Street = string.Empty;
        ZIP = string.Empty;
        City = string.Empty;

        Agents = new List<MetadataAgent>();
        //Pricelists = new List<MetadataPricelist>();
        CustomerNumbers = new List<string>();
    }

    public string Displayname { get; set; }
    public string Role { get; set; }
    public bool IsPriceOnConfirmVisible { get; set; } = true;
    public bool ProcessOrders { get; set; } = false;
    public string DefaultPricelistNumber { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public string Phone { get; set; }
    public string Street { get; set; }
    public string ZIP { get; set; }
    public string City { get; set; }

    public List<MetadataAgent> Agents { get; set; }
    public List<string> CustomerNumbers { get; set; }
    //public List<MetadataPricelist> Pricelists { get; set; }
}
