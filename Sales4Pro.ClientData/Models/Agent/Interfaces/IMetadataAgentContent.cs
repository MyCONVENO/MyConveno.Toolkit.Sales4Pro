namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public interface IMetadataAgentContent
{
    string City { get; set; }
    string DefaultPricelistNumber { get; set; }
    string Displayname { get; set; }
    string Email { get; set; }
    bool IsPriceOnConfirmVisible { get; set; }
    string Mobile { get; set; }
    string Phone { get; set; }
    bool ProcessOrders { get; set; }
    string Role { get; set; }
    string Street { get; set; }
    string ZIP { get; set; }
}