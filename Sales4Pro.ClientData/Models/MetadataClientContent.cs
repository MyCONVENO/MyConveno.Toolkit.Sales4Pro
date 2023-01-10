namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public class MetadataClientContent 
{
    public MetadataClientContent()
    {
        PageProperties = new ClientMetadataPageProperties();
        Catalog = new ClientMetadataCatalog();
        Customer = new ClientMetadataCustomer();
        ShoppingCart = new ClientMetadataShoppingCart();
        ShoppingCartItem = new ClientMetadataShoppingCartItem();
        Reports = new ClientMetadataReports();
    }

    public string AccentColorString { get; set; } = string.Empty;
    public string AccentColorLight1String { get; set; } = string.Empty;
    public string AccentColorLight2String { get; set; } = string.Empty;
    public string AccentColorLight3String { get; set; } = string.Empty;
    public string AccentColorDark1String { get; set; } = string.Empty;
    public string AccentColorDark2String { get; set; } = string.Empty;
    public string AccentColorDark3String { get; set; } = string.Empty;
    public string ImagePathString { get; set; } = string.Empty;

    public string SupportPersonName { get; set; } = string.Empty;
    public string SupportPersonEmail { get; set; } = string.Empty;
    public string SupportPersonPhone { get; set; } = string.Empty;

    public ClientMetadataPageProperties PageProperties { get; set; }
    public ClientMetadataCatalog Catalog { get; set; }
    public ClientMetadataCustomer Customer { get; set; }
    public ClientMetadataShoppingCart ShoppingCart { get; set; }
    public ClientMetadataShoppingCartItem ShoppingCartItem { get; set; }
    public ClientMetadataReports Reports { get; set; }

    public class ClientMetadataPageProperties 
    {
        public string AddressName1 { get; set; } = string.Empty;
        public string AddressName2 { get; set; } = string.Empty;
        public string AddressStreet { get; set; } = string.Empty;
        public string AddressZip { get; set; } = string.Empty;
        public string AddressCity { get; set; } = string.Empty;
        public string AddressCountryName { get; set; } = string.Empty;
        public string AddressCountryCode { get; set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string AddressLine3 { get; set; } = string.Empty;
        public string AddressLine4 { get; set; } = string.Empty;
        public string AddressLine5 { get; set; } = string.Empty;

        public string FooterLeftHeader { get; set; } = string.Empty;
        public string FooterLeftLine1 { get; set; } = string.Empty;
        public string FooterLeftLine2 { get; set; } = string.Empty;
        public string FooterLeftLine3 { get; set; } = string.Empty;
        public string FooterLeftLine4 { get; set; } = string.Empty;
        public string FooterLeftLine5 { get; set; } = string.Empty;

        public string FooterCenterHeader { get; set; } = string.Empty;
        public string FooterCenterLine1 { get; set; } = string.Empty;
        public string FooterCenterLine2 { get; set; } = string.Empty;
        public string FooterCenterLine3 { get; set; } = string.Empty;
        public string FooterCenterLine4 { get; set; } = string.Empty;
        public string FooterCenterLine5 { get; set; } = string.Empty;

        public string FooterRightHeader { get; set; } = string.Empty;
        public string FooterRightLine1 { get; set; } = string.Empty;
        public string FooterRightLine2 { get; set; } = string.Empty;
        public string FooterRightLine3 { get; set; } = string.Empty;
        public string FooterRightLine4 { get; set; } = string.Empty;
        public string FooterRightLine5 { get; set; } = string.Empty;
        public string Addressline { get; set; } = string.Empty;

        public string AEBRemarkText1 { get; set; } = string.Empty;
        public string AEBRemarkText2 { get; set; } = string.Empty;
        public string AEBRemarkText3 { get; set; } = string.Empty;
        public string AEBRemarkText4 { get; set; } = string.Empty;
    }

    public class ClientMetadataCatalog 
    {
        public bool IsCatalogMenuSeasonVisible { get; set; } = true;
        public bool IsCatalogMenuFormVisible { get; set; } = true;
        public bool IsCatalogMenuSingleFilter01Visible { get; set; } = true;
        public bool IsCatalogMenuSingleFilter02Visible { get; set; } = true;
        public bool IsCatalogMenuSingleFilter03Visible { get; set; } = true;
        public bool IsCatalogMenuEASVisible { get; set; } = true;
        public bool IsCatalogMenuEASProductGroupVisible { get; set; } = true;
        public bool IsCatalogMenuEASColorGroupVisible { get; set; } = true;
        public bool IsCatalogMenuCollectionIndicatorVisible { get; set; } = true;
        public bool IsCatalogMenuItemStatusVisible { get; set; } = true;
        public bool IsCatalogMenuSizeVisible { get; set; } = true;
        public bool IsCatalogMenuStockAvailabilityVisible { get; set; } = true;
        public bool IsCatalogMenuHasImageVisible { get; set; } = true;
        public bool IsCatalogMenuHierachyFiltersVisible { get; set; } = true;
        public bool IsCatalogFormFilterDefault { get; set; } = false;
        public bool IsCatalogSingleFilter01Default { get; set; } = false;
        public bool IsCatalogSingleFilter02Default { get; set; } = false;
        public bool ShowColorVariantsInPage { get; set; } = false;
        public string SingleFilter01Header { get; set; } = string.Empty;
        public string SingleFilter02Header { get; set; } = string.Empty;
        public string SingleFilter03Header { get; set; } = string.Empty;
        public string InfotextHeader01 { get; set; } = string.Empty;
        public string InfotextHeader02 { get; set; } = string.Empty;
        public string Header01 { get; set; } = string.Empty;
        public string Header02 { get; set; } = string.Empty;
        public string Header03 { get; set; } = string.Empty;
        public string Header04 { get; set; } = string.Empty;
        public string Header05 { get; set; } = string.Empty;
        public string Header06 { get; set; } = string.Empty;
        public string Header07 { get; set; } = string.Empty;
        public string Header08 { get; set; } = string.Empty;
        public string Header09 { get; set; } = string.Empty;
        public string Header10 { get; set; } = string.Empty;
    }

    public class ClientMetadataCustomer
    {
        public bool IsCustomergroupFilterVisible { get; set; } = false;
        public bool IsCustomerAreaFilterVisible { get; set; } = false;
        public bool IsCustomerAreaSearchDefault { get; set; } = false;
        public bool IsCustomerFavoritesSearchDefault { get; set; } = false;
    }

    public class ClientMetadataShoppingCart 
    {
        public bool IsShoppingCartStockOrderAvailable { get; set; } = true;
        public bool IsShoppingCartSampleOrderAvailable { get; set; } = false;
        public bool IsShoppingCartEditConditionBaseDiscountVisible { get; set; } = true;
        public bool IsShoppingCartEditConditionComplaintDiscountVisible { get; set; } = true;
        public bool IsShoppingCartEditConditionEarlyBirdDiscountVisible { get; set; } = true;
        public bool IsShoppingCartEditConditionPreorderDiscountVisible { get; set; } = true;
        public bool IsShoppingCartEditConditionCollectionDiscountVisible { get; set; } = true;
        public bool IsShoppingCartEditConditionQuantityDiscountVisible { get; set; } = true;
        public bool IsShoppingCartEditConditionChangePaymentMethodVisible { get; set; } = true;
        public bool IsShoppingCartEditConditionChangePaymentTermVisible { get; set; } = true;
        public bool IsShoppingCartEditPreConditionChangePricelistVisible { get; set; } = true;
        public bool IsShoppingCartEditPreConditionDeliveryDecadeVisible { get; set; } = true;
        public bool IsShoppingCartEditCustomerChangePricelistVisible { get; set; } = true;
        public bool IsShoppingCartEditPreConditionChangeDeliveryTypeVisible { get; set; } = true;
        public bool IsShoppingCartEditPreConditionChangeOrderDate { get; set; } = true;
        public bool IsShoppingCartEditConditionChangeValutaVisible { get; set; } = true;
        public bool IsShoppingCartEditConditionBaseDiscountFreeInputVisible { get; set; } = true;
        public bool IsShoppingCartEditConditionComplaintDiscountFreeInputVisible { get; set; } = true;
        public bool IsShoppingCartEditCustomerVatIDMandatory { get; set; } = false;
        public bool IsShoppingCartEditCustomerEMailMandatory { get; set; } = false;
        public bool IsShoppingCartEditCustomerPhoneMandatory { get; set; } = false;
        public bool IsShoppingCartEditCustomerAgentIDFromDefaultAgent { get; set; } = false;
        public bool IsShoppingCartDiscountsFromChangedCustomerOverriden { get; set; } = true;
        public bool IsShoppingCartOrdernumberCustom { get; set; } = false;
        public bool IsShoppingCartDeliveryDateVisible { get; set; } = false;

        public bool IsCustomergroupFilterEnabled { get; set; } = true;
        public bool IsOrderTypeFilterVisible { get; set; } = true;
        public bool IsOrderDateFilterVisible { get; set; } = true;
        public bool IsShoppingCartEditPreconditionsDeliveryDateStartVisible { get; set; } = false;
        public bool IsShoppingCartEditPreconditionsDeliveryDateEndVisible { get; set; } = false;
        public int EditPreconditionsDeliveryDateEndOffset { get; set; } = 0;
        public bool IsStockOrderInNewestSeasonVisible { get; set; } = true;
        public bool IsShoppingCartEditPreconditionsSamplesVisible { get; set; } = true;
        public bool IsShoppingCartEditPreconditionsNoDeliveryBeforeVisible { get; set; } = false;
        public SeasonDefaultDeliveryDate[] SeasonDefaultDeliveryDates { get; set; }
    }

    public class ClientMetadataShoppingCartItem 
    {
        public bool IsShopSetFixassortmentQtyToggleButtonVisible { get; set; } = false;
        public bool IsShopMenuFixAssortmentsVisible { get; set; } = false;
        public bool IsShopFixAssortmentsVisible { get; set; } = false;
        public bool IsShopRulesVisible { get; set; } = true;
        public bool IsShopBarcodescannerEnabled { get; set; } = false;
        public bool IsShoppingCartItemDeliverydateStartFixed { get; set; } = true;
        public bool IsShoppingCartItemModifiedDeliverydateEndIgnored { get; set; } = false;
        public bool IsMatrixEditVisible { get; set; } = false;
        public bool IsEditItemRemarkTextInsideEditable { get; set; } = false;
        public bool IsCheckShoppingCartItemTotalVisible { get; set; } = false;
        public bool IsEditItemFlyoutFreeOfChargeVisible { get; set; } = true;
        public bool IsEditItemFlyoutItemDiscountVisible { get; set; } = true;
        public bool IsEditItemFlyoutItemRemark2Visible { get; set; } = false;
        public bool IsEditItemFlyoutItemEditPriceVisible { get; set; } = false;
        public bool IsEditItemDeliveryDateStartVisible { get; set; } = false;
        public bool IsEditItemDeliveryDateEndVisible { get; set; } = false;
        public bool IsEditItemStockDeliveryDateEndVisible { get; set; } = false;
        public bool IsEditItemDeliveryStartDecadeVisible { get; set; } = true;
        public bool IsEditItemDeliveryDateVisible { get; set; } = false;
        public bool IsEditItemDeliveryDateFromVisible { get; set; } = false;
        public bool IsEditItemFlyoutDeliveryDateVisible { get; set; } = true;
        public bool IsEditItemPlusMinusButtonVisible { get; set; } = false;
        public bool IsCatalogAsMasterArticleGrid { get; set; } = true;

        public int EditItemMaxRemarkTextLength { get; set; } = 999;
        public int EditSampleItemMaxRemarkTextLength { get; set; } = 999;
    }

    public class ClientMetadataReports 
    {
        public bool IsSeasonValueReportEnabled { get; set; } = false;
        public bool IsSeasonCourseReportEnabled { get; set; } = false;
        public bool IsYearValuesReportEnabled { get; set; } = false;
        public bool IsOrderListReportEnabled { get; set; } = false;
    }


    public class SeasonDefaultDeliveryDate
    {
        public string SeasonNumber { get; set; } = string.Empty;
        public DateTime DeliveryDateStart { get; set; }
        public DateTime DeliveryDateEnd { get; set; }
    }
}