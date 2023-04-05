namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public interface IMetadataAgentContent
{
    string DisplayName { get; set; }
    string Street { get; set; }
    string ZIP { get; set; }
    string City { get; set; }
    string Country { get; set; }
    string Phone { get; set; }
    string Mobile { get; set; }
    string Email { get; set; }
    string DefaultPricelistNumber { get; set; }
    bool IsPriceOnConfirmVisible { get; set; }   
}