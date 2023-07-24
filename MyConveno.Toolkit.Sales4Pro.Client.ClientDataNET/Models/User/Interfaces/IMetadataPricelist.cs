namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public interface IMetadataPricelist
{
    string Number { get; set; }
    string Name { get; set; }
    string BuyingCurrency { get; set; }
    string SalesCurrency { get; set; }
}