using MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;
using System.Collections.Generic;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels;

public class SyncMetadataConditions : ISyncMetadataConditions
{
    public string PricelistName { get; set; } = string.Empty;
    public string PricelistNumber { get; set; } = string.Empty;
    public string PricelistBuyingCurrency { get; set; } = string.Empty;
    public string PricelistSalesCurrency { get; set; } = string.Empty;

    public string PaymentTermText { get; set; } = string.Empty;
    public string PaymentTermNumber { get; set; } = string.Empty;

    public string PaymentMethodText { get; set; } = string.Empty;
    public string PaymentMethodNumber { get; set; } = string.Empty;

    public int Valuta { get; set; }
    public bool ShowPrice { get; set; }

    public List<SyncMetadataConditionsDiscount> Discounts { get; set; } = new List<SyncMetadataConditionsDiscount>();

}
