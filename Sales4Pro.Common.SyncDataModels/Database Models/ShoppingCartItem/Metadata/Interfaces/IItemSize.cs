namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels;

public interface IItemSize
{
    string Size { get; set; }
    string EAN { get; set; }
    int Qty { get; set; }
    int StockQty { get; set; }
}