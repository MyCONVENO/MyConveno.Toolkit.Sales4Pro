using System;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;

public interface ISyncMetadataQuantity
{
    double BuyingPrice { get; set; }
    int ComputeTotalQuantity { get; }
    int ComputeTotalStockQuantity { get; }
    string PricelistName { get; set; }
    string BuyingPriceCurrency { get; set; }
    string SalesPriceCurrency { get; set; }
    string CustomerArticleNumber { get; set; }
    DateTime ModifiedDeliveryDateEnd { get; set; }
    DateTime ModifiedDeliveryDateStart { get; set; }
    string Remark01 { get; set; }
    string Remark02 { get; set; }
    double SalesPrice { get; set; }
    double ShoppingCartItemDiscount { get; set; }
    bool ShoppingCartItemIsIdle { get; set; }
    string SizerunName { get; set; }
    bool IsFreeOfCharge { get; set; }
    ItemSize ItemSize01 { get; set; }
    ItemSize ItemSize02 { get; set; }
    ItemSize ItemSize03 { get; set; }
    ItemSize ItemSize04 { get; set; }
    ItemSize ItemSize05 { get; set; }
    ItemSize ItemSize06 { get; set; }
    ItemSize ItemSize07 { get; set; }
    ItemSize ItemSize08 { get; set; }
    ItemSize ItemSize09 { get; set; }
    ItemSize ItemSize10 { get; set; }
    ItemSize ItemSize11 { get; set; }
    ItemSize ItemSize12 { get; set; }
    ItemSize ItemSize13 { get; set; }
    ItemSize ItemSize14 { get; set; }
    ItemSize ItemSize15 { get; set; }
    ItemSize ItemSize16 { get; set; }
    ItemSize ItemSize17 { get; set; }
    ItemSize ItemSize18 { get; set; }
    ItemSize ItemSize19 { get; set; }
    ItemSize ItemSize20 { get; set; }
    ItemSize ItemSize21 { get; set; }
    ItemSize ItemSize22 { get; set; }
    ItemSize ItemSize23 { get; set; }
    ItemSize ItemSize24 { get; set; }
    ItemSize ItemSize25 { get; set; }
    ItemSize ItemSize26 { get; set; }
    ItemSize ItemSize27 { get; set; }
    ItemSize ItemSize28 { get; set; }
    ItemSize ItemSize29 { get; set; }
    ItemSize ItemSize30 { get; set; }
   
}
