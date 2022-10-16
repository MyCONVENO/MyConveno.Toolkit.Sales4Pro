using System;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;

public interface ISyncMetadataColor
{
    string CareLabel { get; set; }
    string Category { get; set; }
    string ColorID { get; set; }
    string ColorImage { get; set; }
    string ColorName { get; set; }
    string ColorNumber { get; set; }
    string ColorText01 { get; set; }
    string ColorText02 { get; set; }
    string ColorText03 { get; set; }
    string ColorText04 { get; set; }
    string ColorText05 { get; set; }
    string ColorText06 { get; set; }
    string ColorText07 { get; set; }
    string ColorText08 { get; set; }
    string ColorText09 { get; set; }
    string ColorText10 { get; set; }
    string ColorTileHexRGB { get; set; }
    string ColorTileImage { get; set; }
    bool IsBlackAndWhiteImage { get; set; }
    bool IsSingleSizeItem { get; set; }
    bool StockArticle { get; set; }
    int StockSum { get; set; }
    DateTime LieferbarAb { get; set; }
    DateTime LieferbarBis { get; set; }

    //void Initialize(SyncShoppingCartItem syncShoppingCartItem);

    //void RecalculateColorPricesAndTotals();

    //Task InitializeSizerunsWithQuantitiesFromSCItem(SyncShoppingCartItem syncShoppingCartItem, string shoppingCartId);

}
