using MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels;

public class SyncMetadataConditionsDiscount : ISyncMetadataConditionsDiscount
{
    public string Name { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public int SignDigit { get; set; } = -1; //Wertart (-1 = Rabatt, +1 = Zuschlag)
    public double DefaultValue { get; set; } = 0.0d;
    public double Value { get; set; } = 0.0d;
    public bool IsIdle { get; set; }
    public bool IsVisible { get; set; }
    public bool IsEnabled { get; set; }
    public bool EliminateOtherHeaderDiscounts { get; set; }
    public bool EliminateItemsDiscounts { get; set; }
}
