using System;

namespace MyConveno.Toolkit.Sales4Pro.Common.ClientData.Interfaces;

public interface IClientMetadataShoppingCart
{
    int EditPreconditionsDeliveryDateEndOffset { get; set; }
    bool IsCustomergroupFilterEnabled { get; set; }
    bool IsOrderDateFilterVisible { get; set; }
    bool IsOrderTypeFilterVisible { get; set; }
    bool IsShoppingCartDeliveryDateVisible { get; set; }
    bool IsShoppingCartDiscountsFromChangedCustomerOverriden { get; set; }
    bool IsShoppingCartEditConditionBaseDiscountFreeInputVisible { get; set; }
    bool IsShoppingCartEditConditionBaseDiscountVisible { get; set; }
    bool IsShoppingCartEditConditionChangePaymentMethodVisible { get; set; }
    bool IsShoppingCartEditConditionChangePaymentTermVisible { get; set; }
    bool IsShoppingCartEditConditionChangeValutaVisible { get; set; }
    bool IsShoppingCartEditConditionCollectionDiscountVisible { get; set; }
    bool IsShoppingCartEditConditionComplaintDiscountFreeInputVisible { get; set; }
    bool IsShoppingCartEditConditionComplaintDiscountVisible { get; set; }
    bool IsShoppingCartEditConditionEarlyBirdDiscountVisible { get; set; }
    bool IsShoppingCartEditConditionPreorderDiscountVisible { get; set; }
    bool IsShoppingCartEditConditionQuantityDiscountVisible { get; set; }
    bool IsShoppingCartEditCustomerAgentIDFromDefaultAgent { get; set; }
    bool IsShoppingCartEditCustomerChangePricelistVisible { get; set; }
    bool IsShoppingCartEditCustomerEMailMandatory { get; set; }
    bool IsShoppingCartEditCustomerPhoneMandatory { get; set; }
    bool IsShoppingCartEditCustomerVatIDMandatory { get; set; }
    bool IsShoppingCartEditPreConditionChangeDeliveryTypeVisible { get; set; }
    bool IsShoppingCartEditPreConditionChangeOrderDate { get; set; }
    bool IsShoppingCartEditPreConditionChangePricelistVisible { get; set; }
    bool IsShoppingCartEditPreConditionDeliveryDecadeVisible { get; set; }
    bool IsShoppingCartEditPreconditionsDeliveryDateEndVisible { get; set; }
    bool IsShoppingCartEditPreconditionsDeliveryDateStartVisible { get; set; }
    bool IsShoppingCartEditPreconditionsNoDeliveryBeforeVisible { get; set; }
    bool IsShoppingCartEditPreconditionsSamplesVisible { get; set; }
    bool IsShoppingCartOrdernumberCustom { get; set; }
    bool IsShoppingCartSampleOrderAvailable { get; set; }
    bool IsShoppingCartStockOrderAvailable { get; set; }
    bool IsStockOrderInNewestSeasonVisible { get; set; }
    SeasonDefaultDeliveryDate[] SeasonDefaultDeliveryDates { get; set; }
}

public class SeasonDefaultDeliveryDate
{
    public string SeasonNumber { get; set; } = string.Empty;
    public DateTime DeliveryDateStart { get; set; }
    public DateTime DeliveryDateEnd { get; set; }
}