﻿namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public class Pricelist : IPricelist
{
    public Pricelist()
    { }

    public string Number { get; set; }
    public string Name { get; set; }
    public string BuyingCurrency { get; set; }
    public string SalesCurrency { get; set; }
}
