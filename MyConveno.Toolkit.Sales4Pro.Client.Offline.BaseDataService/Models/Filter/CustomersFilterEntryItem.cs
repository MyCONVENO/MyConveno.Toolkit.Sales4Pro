namespace MyConveno.Toolkit.Sales4Pro.Client.Offline.BaseDataService;

public class CustomersFilterEntryItem
{
    public CustomersFilterEntryItem()
    {
        FilterHeader = string.Empty;
        FilterEntry = string.Empty;
        FilterLongEntry = string.Empty;
        FilterTextContent = string.Empty;
        FilterDateTimeContent = DateTime.Now;
        FilterIsSticky = false;
        FilterIsVisible = true;
        FilterGroup = CustomersFilterGroupesEnum.Text;
    }

    public CustomersFilterEntryItem(string Header, string entry, string longEntry, CustomersFilterTypesEnum type, string textContent, DateTime datetime, bool isSticky, bool isVisible, CustomersFilterGroupesEnum group, CustomersFilterIconTypesEnum iconType)
    {
        this.FilterHeader = Header;
        this.FilterEntry = entry;
        this.FilterLongEntry = longEntry;
        this.FilterType = type;
        this.FilterTextContent = textContent;
        this.FilterDateTimeContent = datetime;
        this.FilterIsSticky = isSticky;
        this.FilterIsVisible = isVisible;
        this.FilterGroup = group;
    }

    public string FilterHeader { get; set; }
    public string FilterEntry { get; set; }
    public string FilterLongEntry { get; set; }
    public string FilterTextContent { get; set; }
    public DateTime FilterDateTimeContent { get; set; }
    public DateTime FilterDateTimeEndContent { get; set; }
    public bool FilterIsSticky { get; set; }
    public bool FilterIsVisible { get; set; }
    public CustomersFilterTypesEnum FilterType { get; set; }
    public CustomersFilterGroupesEnum FilterGroup { get; set; }
    public CustomersFilterIconTypesEnum FilterIconType { get; set; }

    public override string ToString()
    {
        return this.FilterEntry;
    }

    #region PreSetFilterViewModels

    #region FG_Fallback

    public static CustomersFilterEntryItem GetCustomersByFavoriteFilterEntryViewModel()
    {
        return new CustomersFilterEntryItem("Filter", "Favoriten", "Favoriten", CustomersFilterTypesEnum.Favorite, "Favorites", DateTime.Now, false, true, CustomersFilterGroupesEnum.Favorites, CustomersFilterIconTypesEnum.Search);
    }

    #endregion

    #region FG_Text

    public static CustomersFilterEntryItem GetCustomersByTextFilterEntryViewModel(string QueryText, string QueryLongText)
    {
        return new CustomersFilterEntryItem("Enthält", QueryText, QueryLongText, CustomersFilterTypesEnum.Text, string.Empty, DateTime.Now, false, true, CustomersFilterGroupesEnum.Text, CustomersFilterIconTypesEnum.Search);
    }

    #endregion

    #region FG_Radius

    public static CustomersFilterEntryItem GetRadius10SearchFilterEntryViewModel()
    {
        return new CustomersFilterEntryItem("Radius", "10 km", "10 km", CustomersFilterTypesEnum.Area, "10", DateTime.Now, false, true, CustomersFilterGroupesEnum.Radius, CustomersFilterIconTypesEnum.Search);
    }

    public static CustomersFilterEntryItem GetRadius50SearchFilterEntryViewModel()
    {
        return new CustomersFilterEntryItem("Radius", "50 km", "50 km", CustomersFilterTypesEnum.Area, "50", DateTime.Now, false, true, CustomersFilterGroupesEnum.Radius, CustomersFilterIconTypesEnum.Search);
    }

    public static CustomersFilterEntryItem GetRadius100SearchFilterEntryViewModel()
    {
        return new CustomersFilterEntryItem("Radius", "100 km", "100 km", CustomersFilterTypesEnum.Area, "100", DateTime.Now, false, true, CustomersFilterGroupesEnum.Radius, CustomersFilterIconTypesEnum.Search);
    }

    #endregion

    #region FG_Association

    public static CustomersFilterEntryItem GetAssociationFilterEntryViewModel(string textContent, string QueryText, string QueryLongText)
    {
        return new CustomersFilterEntryItem("Vereinigung", QueryText, QueryLongText, CustomersFilterTypesEnum.Association, textContent, DateTime.Now, false, true, CustomersFilterGroupesEnum.Association, CustomersFilterIconTypesEnum.Search);
    }

    #endregion

    #region IsFavorite

    public static CustomersFilterEntryItem GetIsFavoriteFilterEntryViewModel()
    {
        return new CustomersFilterEntryItem("Favorit", "Favoriten", "Favoriten", CustomersFilterTypesEnum.Favorite, string.Empty, DateTime.Now, false, true, CustomersFilterGroupesEnum.Favorites, CustomersFilterIconTypesEnum.Filter);
    }

    #endregion

    #endregion

}

public enum CustomersFilterTypesEnum { Single01, Single02, Single03, Association, Area, Barcode, Text, All, Label, Season, EASColor, Color, ArticleCollection, EASMaterial, EASType, ColorStatus, Form, Favorite, ColorID, CustomerNumber, CustomerID, InStock, ArticleID, OrderType, Size, Date, DaysBack, ShoppingCart, HierarchyFilter01, HierarchyFilter02, HierarchyFilter03, HierarchyFilter04, HierarchyFilter05, Customergroup, OrderDate, HasColorVariantImage }
public enum CustomersFilterGroupesEnum { PreFilter, Favorites, Form, EASType, EASColor, Single01, Hierarchy01, Hierarchy02, Hierarchy03, Hierarchy04, Hierarchy05, Association, Radius, InStock, Text, Label, Season, OrderType, OrderDate }
public enum CustomersFilterIconTypesEnum { Filter, Search }
