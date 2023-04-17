namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public class MetadataPricelist : IMetadataPricelist
{
    public MetadataPricelist()
    { }

    public string Number { get; set; }
    public string Name { get; set; }
    public string BuyingCurrency { get; set; }
    public string SalesCurrency { get; set; }
}
