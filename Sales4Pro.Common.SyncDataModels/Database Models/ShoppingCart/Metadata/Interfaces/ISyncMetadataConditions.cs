using System.Collections.Generic;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;

public interface ISyncMetadataConditions
{
    string PricelistNumber { get; set; }
    string PricelistName { get; set; }
    string PricelistBuyingCurrency { get; set; }
    string PricelistSalesCurrency { get; set; }

    string PaymentTermNumber { get; set; }
    string PaymentTermText { get; set; }

    string PaymentMethodNumber { get; set; }
    string PaymentMethodText { get; set; }

    int Valuta { get; set; }
    bool ShowPrice { get; set; }

    List<SyncMetadataConditionsDiscount> Discounts { get; set; }
}