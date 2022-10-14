namespace MyConveno.Toolkit.Sales4Pro.Common.ClientData.Interfaces;

public interface IClientMetadataShoppingCartItem
{
    int EditItemMaxRemarkTextLength { get; set; }
    int EditSampleItemMaxRemarkTextLength { get; set; }
    bool IsCatalogAsMasterArticleGrid { get; set; }
    bool IsCheckShoppingCartItemTotalVisible { get; set; }
    bool IsEditItemDeliveryDateEndVisible { get; set; }
    bool IsEditItemDeliveryDateFromVisible { get; set; }
    bool IsEditItemDeliveryDateStartVisible { get; set; }
    bool IsEditItemDeliveryDateVisible { get; set; }
    bool IsEditItemDeliveryStartDecadeVisible { get; set; }
    bool IsEditItemFlyoutDeliveryDateVisible { get; set; }
    bool IsEditItemFlyoutFreeOfChargeVisible { get; set; }
    bool IsEditItemFlyoutItemDiscountVisible { get; set; }
    bool IsEditItemFlyoutItemEditPriceVisible { get; set; }
    bool IsEditItemFlyoutItemRemark2Visible { get; set; }
    bool IsEditItemPlusMinusButtonVisible { get; set; }
    bool IsEditItemRemarkTextInsideEditable { get; set; }
    bool IsEditItemStockDeliveryDateEndVisible { get; set; }
    bool IsMatrixEditVisible { get; set; }
    bool IsShopBarcodescannerEnabled { get; set; }
    bool IsShopFixAssortmentsVisible { get; set; }
    bool IsShopMenuFixAssortmentsVisible { get; set; }
    bool IsShoppingCartItemDeliverydateStartFixed { get; set; }
    bool IsShoppingCartItemModifiedDeliverydateEndIgnored { get; set; }
    bool IsShopRulesVisible { get; set; }
    bool IsShopSetFixassortmentQtyToggleButtonVisible { get; set; }
}