namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels;

public class ItemSize : IItemSize
{
    public string Size { get; set; } = string.Empty;
    public string EAN { get; set; } = string.Empty;
    public int Qty { get; set; } = 0;
    public int StockQty { get; set; } = 0;
}

