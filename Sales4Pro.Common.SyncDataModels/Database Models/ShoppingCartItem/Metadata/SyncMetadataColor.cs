using MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;
using System;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels;

public class SyncMetadataColor : ISyncMetadataColor
{
    public string ColorID { get; set; }
    public string ColorNumber { get; set; }
    public string ColorName { get; set; }
    public bool StockArticle { get; set; }
    public string ColorImage { get; set; }
    public bool IsBlackAndWhiteImage { get; set; }
    public string ColorTileImage { get; set; }
    public string ColorTileHexRGB { get; set; }
    public int StockSum { get; set; }
    public bool IsSingleSizeItem { get; set; }
    public string ColorText01 { get; set; }
    public string ColorText02 { get; set; }
    public string ColorText03 { get; set; }
    public string ColorText04 { get; set; }
    public string ColorText05 { get; set; }
    public string ColorText06 { get; set; }
    public string ColorText07 { get; set; }
    public string ColorText08 { get; set; }
    public string ColorText09 { get; set; }
    public string ColorText10 { get; set; }
    public string CareLabel { get; set; }
    public string Category { get; set; }
    public DateTime LieferbarAb { get; set; }
    public DateTime LieferbarBis { get; set; }
}