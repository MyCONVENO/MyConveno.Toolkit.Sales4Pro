namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public class MetadataAgentContent : IMetadataAgentContent
{
    public MetadataAgentContent()
    {
        DisplayName = string.Empty;
        Street = string.Empty;
        ZIP = string.Empty;
        City = string.Empty;
        Country = string.Empty;
        Mobile = string.Empty;
        Phone = string.Empty;
        Email = string.Empty;
        ConfirmationEmail = string.Empty;
        DefaultPricelistNumber = "-";
        IsPriceOnConfirmVisible = true;
    }

    public string DisplayName { get; set; }
    public string Street { get; set; }
    public string ZIP { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Mobile { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string ConfirmationEmail { get; set; }
    public string DefaultPricelistNumber { get; set; }
    public bool IsPriceOnConfirmVisible { get; set; } = true;
}