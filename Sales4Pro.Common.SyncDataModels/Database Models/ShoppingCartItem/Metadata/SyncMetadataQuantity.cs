using Newtonsoft.Json;
using MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;
using System;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels;

public class SyncMetadataQuantity : ISyncMetadataQuantity
{
    public string SizerunName { get; set; }
    public string CustomerArticleNumber { get; set; }
    public string Remark01 { get; set; }
    public string Remark02 { get; set; }
    public double ShoppingCartItemDiscount { get; set; } = 0d;
    public bool ShoppingCartItemIsIdle { get; set; } = false;
    public DateTime ModifiedDeliveryDateStart { get; set; } = new DateTime(1950, 1, 1);
    public DateTime ModifiedDeliveryDateEnd { get; set; } = new DateTime(2100, 1, 1);
    public bool IsFreeOfCharge { get; set; } = false;

    public string PricelistName { get; set; } = string.Empty;
    public double BuyingPrice { get; set; } = 0d;
    public double SalesPrice { get; set; } = 0d;
    public string BuyingPriceCurrency { get; set; }
    public string SalesPriceCurrency { get; set; }

    public ItemSize ItemSize01 { get; set; }
    public ItemSize ItemSize02 { get; set; }
    public ItemSize ItemSize03 { get; set; }
    public ItemSize ItemSize04 { get; set; }
    public ItemSize ItemSize05 { get; set; }
    public ItemSize ItemSize06 { get; set; }
    public ItemSize ItemSize07 { get; set; }
    public ItemSize ItemSize08 { get; set; }
    public ItemSize ItemSize09 { get; set; }
    public ItemSize ItemSize10 { get; set; }
    public ItemSize ItemSize11 { get; set; }
    public ItemSize ItemSize12 { get; set; }
    public ItemSize ItemSize13 { get; set; }
    public ItemSize ItemSize14 { get; set; }
    public ItemSize ItemSize15 { get; set; }
    public ItemSize ItemSize16 { get; set; }
    public ItemSize ItemSize17 { get; set; }
    public ItemSize ItemSize18 { get; set; }
    public ItemSize ItemSize19 { get; set; }
    public ItemSize ItemSize20 { get; set; }
    public ItemSize ItemSize21 { get; set; }
    public ItemSize ItemSize22 { get; set; }
    public ItemSize ItemSize23 { get; set; }
    public ItemSize ItemSize24 { get; set; }
    public ItemSize ItemSize25 { get; set; }
    public ItemSize ItemSize26 { get; set; }
    public ItemSize ItemSize27 { get; set; }
    public ItemSize ItemSize28 { get; set; }
    public ItemSize ItemSize29 { get; set; }
    public ItemSize ItemSize30 { get; set; }

    [JsonIgnore]
    public int ComputeTotalQuantity
    {
        get
        {
            int runTotal = 0;

            runTotal += ItemSize01 == null ? 0 : ItemSize01.Qty;
            runTotal += ItemSize02 == null ? 0 : ItemSize02.Qty;
            runTotal += ItemSize03 == null ? 0 : ItemSize03.Qty;
            runTotal += ItemSize04 == null ? 0 : ItemSize04.Qty;
            runTotal += ItemSize05 == null ? 0 : ItemSize05.Qty;
            runTotal += ItemSize06 == null ? 0 : ItemSize06.Qty;
            runTotal += ItemSize07 == null ? 0 : ItemSize07.Qty;
            runTotal += ItemSize08 == null ? 0 : ItemSize08.Qty;
            runTotal += ItemSize09 == null ? 0 : ItemSize09.Qty;
            runTotal += ItemSize10 == null ? 0 : ItemSize10.Qty;
            runTotal += ItemSize11 == null ? 0 : ItemSize11.Qty;
            runTotal += ItemSize12 == null ? 0 : ItemSize12.Qty;
            runTotal += ItemSize13 == null ? 0 : ItemSize13.Qty;
            runTotal += ItemSize14 == null ? 0 : ItemSize14.Qty;
            runTotal += ItemSize15 == null ? 0 : ItemSize15.Qty;
            runTotal += ItemSize16 == null ? 0 : ItemSize16.Qty;
            runTotal += ItemSize17 == null ? 0 : ItemSize17.Qty;
            runTotal += ItemSize18 == null ? 0 : ItemSize18.Qty;
            runTotal += ItemSize19 == null ? 0 : ItemSize19.Qty;
            runTotal += ItemSize20 == null ? 0 : ItemSize20.Qty;
            runTotal += ItemSize21 == null ? 0 : ItemSize21.Qty;
            runTotal += ItemSize22 == null ? 0 : ItemSize22.Qty;
            runTotal += ItemSize23 == null ? 0 : ItemSize23.Qty;
            runTotal += ItemSize24 == null ? 0 : ItemSize24.Qty;
            runTotal += ItemSize25 == null ? 0 : ItemSize25.Qty;
            runTotal += ItemSize26 == null ? 0 : ItemSize26.Qty;
            runTotal += ItemSize27 == null ? 0 : ItemSize27.Qty;
            runTotal += ItemSize28 == null ? 0 : ItemSize28.Qty;
            runTotal += ItemSize29 == null ? 0 : ItemSize29.Qty;
            runTotal += ItemSize30 == null ? 0 : ItemSize30.Qty;
            return runTotal;
        }
    }

    [JsonIgnore]
    public int ComputeTotalStockQuantity
    {
        get
        {
            int runTotal = 0;

            runTotal += ItemSize01 == null ? 0 : ItemSize01.StockQty;
            runTotal += ItemSize02 == null ? 0 : ItemSize02.StockQty;
            runTotal += ItemSize03 == null ? 0 : ItemSize03.StockQty;
            runTotal += ItemSize04 == null ? 0 : ItemSize04.StockQty;
            runTotal += ItemSize05 == null ? 0 : ItemSize05.StockQty;
            runTotal += ItemSize06 == null ? 0 : ItemSize06.StockQty;
            runTotal += ItemSize07 == null ? 0 : ItemSize07.StockQty;
            runTotal += ItemSize08 == null ? 0 : ItemSize08.StockQty;
            runTotal += ItemSize09 == null ? 0 : ItemSize09.StockQty;
            runTotal += ItemSize10 == null ? 0 : ItemSize10.StockQty;
            runTotal += ItemSize11 == null ? 0 : ItemSize11.StockQty;
            runTotal += ItemSize12 == null ? 0 : ItemSize12.StockQty;
            runTotal += ItemSize13 == null ? 0 : ItemSize13.StockQty;
            runTotal += ItemSize14 == null ? 0 : ItemSize14.StockQty;
            runTotal += ItemSize15 == null ? 0 : ItemSize15.StockQty;
            runTotal += ItemSize16 == null ? 0 : ItemSize16.StockQty;
            runTotal += ItemSize17 == null ? 0 : ItemSize17.StockQty;
            runTotal += ItemSize18 == null ? 0 : ItemSize18.StockQty;
            runTotal += ItemSize19 == null ? 0 : ItemSize19.StockQty;
            runTotal += ItemSize20 == null ? 0 : ItemSize20.StockQty;
            runTotal += ItemSize21 == null ? 0 : ItemSize21.StockQty;
            runTotal += ItemSize22 == null ? 0 : ItemSize22.StockQty;
            runTotal += ItemSize23 == null ? 0 : ItemSize23.StockQty;
            runTotal += ItemSize24 == null ? 0 : ItemSize24.StockQty;
            runTotal += ItemSize25 == null ? 0 : ItemSize25.StockQty;
            runTotal += ItemSize26 == null ? 0 : ItemSize26.StockQty;
            runTotal += ItemSize27 == null ? 0 : ItemSize27.StockQty;
            runTotal += ItemSize28 == null ? 0 : ItemSize28.StockQty;
            runTotal += ItemSize29 == null ? 0 : ItemSize29.StockQty;
            runTotal += ItemSize30 == null ? 0 : ItemSize30.StockQty;
            return runTotal;
        }
    }

    //public class ItemSize
    //{
    //    public string Size { get; set; }= string.Empty;
    //    public string EAN { get; set; }= string.Empty;
    //    public int Qty { get; set; } = 0;
    //    public int StockQty { get; set; } = 0;
    //}

}