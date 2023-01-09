namespace MyConveno.Toolkit.Sales4Pro.Client.Offline.BaseDataService;

public enum CatalogFilterTypesEnum { OrderType, Catalog, Season, Single01, Contains01, FreeText }

public enum CatalogFilterGroupesEnum { PreFilter, PostFilter, SearchFilters }

public enum CatalogFilterIconTypesEnum { Filter, Search }

public record CatalogFilterEntryItem
{
    public CatalogFilterEntryItem()
    {
        FilterHeader = string.Empty;
        FilterEntry = string.Empty;
        FilterLongEntry = string.Empty;
        FilterType = CatalogFilterTypesEnum.OrderType;
        FilterTextContent = string.Empty;
        FilterDateTimeContent = DateTime.Now;
        FilterImagePath = string.Empty;
        FilterIsSticky = false;
        FilterIsVisible = true;
        FilterGroup = CatalogFilterGroupesEnum.SearchFilters;
    }

    public CatalogFilterEntryItem(string Header,
                                  string entry,
                                  string longEntry,
                                  CatalogFilterTypesEnum type,
                                  string textContent,
                                  string imagePath,
                                  DateTime datetime,
                                  bool isSticky,
                                  bool isVisible,
                                  CatalogFilterGroupesEnum group,
                                  CatalogFilterIconTypesEnum iconType)
    {
        FilterHeader = Header;
        FilterEntry = entry;
        FilterLongEntry = longEntry;
        FilterType = type;
        FilterTextContent = textContent;
        FilterImagePath = imagePath;
        FilterDateTimeContent = datetime;
        FilterIsSticky = isSticky;
        FilterIsVisible = isVisible;
        FilterGroup = group;
    }

    public string FilterHeader;

    public string FilterEntry { get; init; }
    public string FilterLongEntry { get; init; }
    public string FilterTextContent { get; init; }
    public string FilterImagePath { get; init; }
    public DateTime FilterDateTimeContent { get; init; }
    public DateTime filterDateTimeEndContent { get; init; }
    public bool FilterIsSticky { get; init; }
    public bool FilterIsVisible { get; init; }

    public CatalogFilterTypesEnum FilterType { get; init; }
    public CatalogFilterGroupesEnum FilterGroup { get; init; }
    public CatalogFilterIconTypesEnum filterIconType { get; init; }

    public override string ToString()
    {
        return this.FilterEntry;
    }

    #region PreSetFilterViewModels

    //public static CatalogFilterEntryItem GetCatalogByInStockFilterEntryViewModel()
    //{
    //    return new CatalogFilterEntryItem("Verfügbarkeit", "Sofort lieferbar", "Sofort lieferbar", CatalogFilterTypesEnum.InStock, "InStock", string.Empty, DateTime.Now, true, true, CatalogFilterGroupesEnum.PostFilter, CatalogFilterIconTypesEnum.Filter);
    //}

    //public static CatalogFilterEntryItem GetCatalogByHasColorImageFilterEntryViewModel()
    //{
    //    return new CatalogFilterEntryItem("Filter", "Mit Farbbild", "Mit Farbbild", CatalogFilterTypesEnum.HasImage, "Mit Farbbild", string.Empty, DateTime.Now, false, true, CatalogFilterGroupesEnum.PostFilter, CatalogFilterIconTypesEnum.Search);
    //}

    public static CatalogFilterEntryItem GetCatalogByTextFilterEntryViewModel(string QueryText, string QueryLongText)
    {
        return new CatalogFilterEntryItem("Enthält", QueryText, QueryLongText, CatalogFilterTypesEnum.FreeText, string.Empty, string.Empty, DateTime.Now, false, true, CatalogFilterGroupesEnum.SearchFilters, CatalogFilterIconTypesEnum.Search);
    }

    //public static CatalogFilterEntryItem GetCatalogByColorIdFilterEntryViewModel(string colorId)
    //{
    //    return new CatalogFilterEntryItem("Enthält", colorId, colorId, CatalogFilterTypesEnum.ColorID, colorId, string.Empty, DateTime.Now, false, true, CatalogFilterGroupesEnum.Text, CatalogFilterIconTypesEnum.Search);
    //}

    //public static CatalogFilterEntryItem GetCatalogByHasColorVariantImageFilterEntryViewModel()
    //{
    //    return new CatalogFilterEntryItem("Enthält", "Mit Farbbild", "Mit Farbbild", CatalogFilterTypesEnum.HasColorVariantImage, "HasColorVariantImage", string.Empty, DateTime.Now, false, true, CatalogFilterGroupesEnum.ColorVariants, CatalogFilterIconTypesEnum.Search);
    //}

    #endregion

}
