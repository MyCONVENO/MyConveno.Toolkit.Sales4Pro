namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;

public interface ISyncMetadataConditionsDiscount
{
    string Name { get; set; }
    string Id { get; set; }
    int SignDigit { get; set; } //Wertart (-1 = Rabatt, +1 = Zuschlag)
    double DefaultValue { get; set; }
    double Value { get; set; }
    bool IsEnabled { get; set; }
}
