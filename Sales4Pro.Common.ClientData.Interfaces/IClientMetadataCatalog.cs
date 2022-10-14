namespace MyConveno.Toolkit.Sales4Pro.Common.ClientData.Interfaces;

public interface IClientMetadataCatalog
{
    string Header01 { get; set; }
    string Header02 { get; set; }
    string Header03 { get; set; }
    string Header04 { get; set; }
    string Header05 { get; set; }
    string Header06 { get; set; }
    string Header07 { get; set; }
    string Header08 { get; set; }
    string Header09 { get; set; }
    string Header10 { get; set; }
    string InfotextHeader01 { get; set; }
    string InfotextHeader02 { get; set; }
    bool IsCatalogFormFilterDefault { get; set; }
    bool IsCatalogMenuCollectionIndicatorVisible { get; set; }
    bool IsCatalogMenuEASColorGroupVisible { get; set; }
    bool IsCatalogMenuEASProductGroupVisible { get; set; }
    bool IsCatalogMenuEASVisible { get; set; }
    bool IsCatalogMenuFormVisible { get; set; }
    bool IsCatalogMenuHasImageVisible { get; set; }
    bool IsCatalogMenuHierachyFiltersVisible { get; set; }
    bool IsCatalogMenuItemStatusVisible { get; set; }
    bool IsCatalogMenuSeasonVisible { get; set; }
    bool IsCatalogMenuSingleFilter01Visible { get; set; }
    bool IsCatalogMenuSingleFilter02Visible { get; set; }
    bool IsCatalogMenuSingleFilter03Visible { get; set; }
    bool IsCatalogMenuSizeVisible { get; set; }
    bool IsCatalogMenuStockAvailabilityVisible { get; set; }
    bool IsCatalogSingleFilter01Default { get; set; }
    bool IsCatalogSingleFilter02Default { get; set; }
    bool ShowColorVariantsInPage { get; set; }
    string SingleFilter01Header { get; set; }
    string SingleFilter02Header { get; set; }
    string SingleFilter03Header { get; set; }
}