namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData
{
    public interface IPricelist
    {
        string BuyingCurrency { get; set; }
        string Name { get; set; }
        string Number { get; set; }
        string SalesCurrency { get; set; }
    }
}