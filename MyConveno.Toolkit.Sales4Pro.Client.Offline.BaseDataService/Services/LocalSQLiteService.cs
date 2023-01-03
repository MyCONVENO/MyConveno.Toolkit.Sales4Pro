using Dapper;
using Microsoft.Data.Sqlite;
using System.Text;

namespace MyConveno.Toolkit.Sales4Pro.Client.Offline.BaseDataService;

public class LocalSQLiteService : ILocalSQLiteService
{
    public SqliteConnection Connection { get; set; } = null;

    public LocalSQLiteService(string dbFilename)
    {
        Connection = new SqliteConnection("Data Source=" + dbFilename);
    }

    public async Task InitializeAsync()
    { }

    #region Assets

    public async Task<Asset> GetAssetAsync()
    {
        await InitializeAsync();

        if (Connection is null)
            return new Asset();

        Asset? asset = (await Connection.QueryAsync<Asset>("SELECT * FROM Asset"))
                                        .FirstOrDefault();

        return asset is not null ? asset : new Asset();
    }

    #endregion

    #region Customers

    public async Task<int> GetCustomersCountAsync()
    {
        await InitializeAsync();
        return await Connection.ExecuteScalarAsync<int>("Select Count(*) FROM Customer");
    }

    public virtual async Task<IList<Customer>> GetCustomersAsync(List<CustomersFilterEntryItem> filterList)
    {
        await InitializeAsync();

        try
        {
            StringBuilder sbSelect = new();
            sbSelect.Append("SELECT ");
            sbSelect.Append("Customer.CustomerID, ");
            sbSelect.Append("Customer.PricelistID, ");
            sbSelect.Append("Customer.AgentNumber, ");
            sbSelect.Append("Customer.Metadata AS CustomerMetadata, ");
            sbSelect.Append("Customer.HistoryMetadata AS HistoryMetadata, ");
            sbSelect.Append("Customer.CustomerName, ");
            sbSelect.Append("Customer.CustomerNumber, ");
            sbSelect.Append("Customer.StartsWithFilter01, ");
            sbSelect.Append("Customer.StartsWithFilter02, ");
            sbSelect.Append("Customer.StartsWithFilter03, ");
            sbSelect.Append("Customer.ContainsFilter01, ");
            sbSelect.Append("Customer.ContainsFilter02, ");
            sbSelect.Append("Customer.ContainsFilter03, ");
            sbSelect.Append("Customer.Latitude, ");
            sbSelect.Append("Customer.Longitude ");
            sbSelect.Append("FROM Customer ");
            sbSelect.Append("WHERE ");

            bool hasTrailingAND = false;

            foreach (CustomersFilterEntryItem f in filterList)
            {
                switch (f.FilterType)
                {
                    case CustomersFilterTypesEnum.All:
                        break;
                    //case CustomersFilterTypesEnum.Favorite:
                    //    sbSelect.Append("Customer.PricelistID <> 'xxxxx' AND "); // eigentlich nur ein Dummy, der immer true zurückgibt
                    //    break;
                    //case CustomersFilterTypesEnum.Association:
                    //    List<AssociationMember> members = await sales4ProDatabaseConnection.QueryAsync<AssociationMember>("Select * FROM AssociationMember WHERE (AssociationMember.AssociationMemberAssociationID = '" + f.FilterTextContent + "')");
                    //    if (members.Any())
                    //    {
                    //        wherePart += "(";
                    //        foreach (AssociationMember m in members)
                    //        {
                    //            wherePart += "(Customer.CustomerID = '" + m.AssociationMemberCustomerID + "') OR ";
                    //        }
                    //        wherePart = wherePart.Remove(wherePart.Length - 4);
                    //        wherePart += ") AND ";
                    //    }
                    //    break;
                    //case CustomersFilterTypesEnum.Customergroup:
                    //    sbSelect.Append("(Customer.Customergroup = '" + f.FilterTextContent + "') AND ");
                    //    break;
                    case CustomersFilterTypesEnum.CustomerID:
                        sbSelect.Append("(Customer.CustomerID = '" + f.FilterTextContent + "') AND ");
                        break;
                    case CustomersFilterTypesEnum.CustomerNumber:
                        sbSelect.Append("(Customer.CustomerNumber = '" + f.FilterTextContent + "') AND ");
                        break;
                        //case CustomersFilterTypesEnum.Text:
                        //    if (string.IsNullOrEmpty(f.FilterEntry))
                        //        return new List<CustomerPricelist_Join>();
                        //    //wherePart += "(CustomerCity LIKE '" + f.FilterEntry + "%' OR CustomerName2 LIKE '%" + f.FilterEntry + "%' OR CustomerNumber = '" + f.FilterEntry + "' OR CustomerZIP LIKE '" + f.FilterEntry + "%') AND";
                        //    StringBuilder sbWhere1 = new StringBuilder();
                        //    sbWhere1.Append("(CustomerNumber = '" + f.FilterEntry + "' ");
                        //    sbWhere1.Append("OR StartsWithFilter01 LIKE '" + f.FilterEntry + "%' ");
                        //    sbWhere1.Append("OR StartsWithFilter02 LIKE '" + f.FilterEntry + "%' ");
                        //    sbWhere1.Append("OR StartsWithFilter03 LIKE '" + f.FilterEntry + "%' ");
                        //    sbWhere1.Append("OR ContainsFilter01 LIKE '%" + f.FilterEntry + "%' ");
                        //    sbWhere1.Append("OR ContainsFilter02 LIKE '%" + f.FilterEntry + "%' ");
                        //    sbWhere1.Append("OR ContainsFilter03 LIKE '%" + f.FilterEntry + "%') ");
                        //    sbWhere1.Append(" AND ");
                        //    sbSelect.Append(sbWhere1.ToString());

                        //    //wherePart += "( CustomerNumber = '" + f.FilterEntry + "' OR StartsWithFilter01 LIKE '" + f.FilterEntry + "%' OR Filter02 LIKE '%" + f.FilterEntry + "%') AND";
                        //    break;
                        //case CustomersFilterTypesEnum.Area:
                        //    double radius = Convert.ToDouble(f.FilterTextContent);
                        //    double latitude = 0.0;
                        //    double longitude = 0.0;
                        //    Geolocator gl = new Geolocator();
                        //    Geoposition gp = null;
                        //    try
                        //    {
                        //        gp = await gl.GetGeopositionAsync(TimeSpan.FromMinutes(3), TimeSpan.FromMinutes(1));
                        //    }
                        //    catch
                        //    {
                        //        //Positionsbestimmung deaktiviert.
                        //        return new List<CustomerPricelist_Join>();
                        //    }

                        //    if (gp != null)
                        //    {
                        //        latitude = gp.Coordinate.Point.Position.Latitude;
                        //        longitude = gp.Coordinate.Point.Position.Longitude;
                        //    }

                        //    gl = null;
                        //    gp = null;

                        //    double latoffset = 1.0 / 111.3 * radius;
                        //    double lonoffset = 1.0 / (111.3 * Math.Cos(ValueHelper.DegreeToRadian(latitude))) * radius;

                        //    sbSelect.Append("(Latitude <= " + (latitude + latoffset).ToString().Replace(",", ".") + " AND Latitude >= " + (latitude - latoffset).ToString().Replace(",", ".") + " AND Longitude <= " + (longitude + lonoffset).ToString().Replace(",", ".") + " AND Longitude >= " + (longitude - lonoffset).ToString().Replace(",", ".") + ") AND ");
                        //    break;
                }
                hasTrailingAND = true;
            }

            if (hasTrailingAND)
                sbSelect.Remove(sbSelect.Length - 4, 4);  // Entferne ggf. das letzte "AND" aus dem Select String
            else
                sbSelect.Remove(sbSelect.Length - 6, 6);  // Entferne ggf. das "WHERE" aus dem Select String

            string selectstring = sbSelect.ToString();

            IList<Customer> cust = (await Connection.QueryAsync<Customer>(selectstring)).ToList();

            return cust;
        }
        catch (Exception)
        {
            return new List<Customer>();
        }
    }

    public async Task<Customer> GetCustomerAsync(string customerNumber)
    {
        await InitializeAsync();

        StringBuilder sbSelect = new();
        sbSelect.Append("SELECT ");
        sbSelect.Append("Customer.CustomerID, ");
        sbSelect.Append("Customer.PricelistID, ");
        sbSelect.Append("Customer.AgentNumber, ");
        sbSelect.Append("Customer.Metadata AS CustomerMetadata, ");
        sbSelect.Append("Customer.HistoryMetadata AS HistoryMetadata, ");
        sbSelect.Append("Customer.CustomerName, ");
        sbSelect.Append("Customer.CustomerNumber, ");
        sbSelect.Append("Customer.StartsWithFilter01, ");
        sbSelect.Append("Customer.StartsWithFilter02, ");
        sbSelect.Append("Customer.StartsWithFilter03, ");
        sbSelect.Append("Customer.ContainsFilter01, ");
        sbSelect.Append("Customer.ContainsFilter02, ");
        sbSelect.Append("Customer.ContainsFilter03, ");
        sbSelect.Append("Customer.Latitude, ");
        sbSelect.Append("Customer.Longitude ");
        sbSelect.Append("FROM Customer ");
        sbSelect.Append("WHERE Customer.CustomerNumber = '" + customerNumber + "'");

        Customer? customer = (await Connection.QueryAsync<Customer>(sbSelect.ToString())).FirstOrDefault();

        return customer ?? new Customer();
    }

    #endregion

    #region Articles and Colors

    public async Task<int> GetArticlesCountAsync()
    {
        await InitializeAsync();

        return await Connection.ExecuteScalarAsync<int>("Select Count(ArticleID) FROM Article");
    }

    public virtual async Task<Article> GetArticleAsync(string articleId)
    {
        await InitializeAsync();

        Article article = (await Connection.QuerySingleAsync<Article>("SELECT * FROM Article Where ArticleID =" + articleId));
        return article ?? new Article();
    }

    public virtual async Task<List<ArticleColor>> GetDBShoppingCartItemsByArticleNumberAsync(string articleNumber,
                                                                                                   string labelNumber,
                                                                                                   string category,
                                                                                                   bool stockOnly)
    {
        await InitializeAsync();

        StringBuilder sbSelect = new();
        sbSelect.Append("SELECT ");
        sbSelect.Append("Article.SeasonNumber, ");
        sbSelect.Append("Article.LabelNumber, ");
        sbSelect.Append("Article.ArticleID AS ArticleID, ");
        sbSelect.Append("Article.ArticleNumber AS ArticleNumber, ");
        sbSelect.Append("Article.ArticleName AS ArticleName, ");
        sbSelect.Append("Article.Metadata AS ArticleMetadataJSON, ");
        sbSelect.Append("Color.ColorID, ");
        sbSelect.Append("Color.ColorNumber, ");
        sbSelect.Append("Color.ColorName, ");
        sbSelect.Append("Color.Category, ");
        sbSelect.Append("Color.Metadata AS ColorMetadataJSON, ");
        sbSelect.Append("Color.StockArticle, ");
        sbSelect.Append("Color.HasImage ");
        sbSelect.Append("FROM Article ");
        sbSelect.Append("INNER JOIN Color ");
        sbSelect.Append("ON Article.ArticleNumber = Color.ArticleNumber ");
        sbSelect.Append("AND Article.LabelNumber = Color.LabelNumber ");
        sbSelect.Append("AND Article.SingleFilter01 = Color.Category ");
        sbSelect.Append(string.Format("WHERE Article.ArticleNumber = '{0}' ", articleNumber));
        sbSelect.Append("AND ");
        sbSelect.Append(string.Format("Article.LabelNumber = '{0}' ", labelNumber));
        sbSelect.Append("AND ");
        sbSelect.Append(string.Format("Article.SingleFilter01 = '{0}' ", category));

        if (stockOnly)
        { sbSelect.Append("AND Color.StockArticle = 1"); }

        IEnumerable<ArticleColor> sqliteResultlist = await Connection.QueryAsync<ArticleColor>(sbSelect.ToString());
        return sqliteResultlist.ToList() ?? new List<ArticleColor>();
    }

    public virtual async Task<string> GetMetadataArticleColorSizerunJSONStringAsync(string seasonnumber,
                                                                                    string articlenumber,
                                                                                    string colornumber)
    {
        await InitializeAsync();

        StringBuilder sbSelect = new();
        sbSelect.Append("SELECT ");
        sbSelect.Append("Metadata AS ColorMetadataJSON ");
        sbSelect.Append("FROM Color ");
        sbSelect.Append(string.Format("WHERE SeasonNumber = '{0}' ", seasonnumber));
        sbSelect.Append("AND ");
        sbSelect.Append(string.Format("ArticleNumber = '{0}' ", articlenumber));
        sbSelect.Append("AND ");
        sbSelect.Append(string.Format("ColorNumber = '{0}'", colornumber));

        IEnumerable<string> sqliteResultlist = await Connection.QueryAsync<string>(sbSelect.ToString());

        if (sqliteResultlist is null) return string.Empty;

        return sqliteResultlist.FirstOrDefault() ?? string.Empty;
    }


    /// <summary>
    // -----------------------------------------------------------------------
    /// ArtikelIds Suchen
    // -----------------------------------------------------------------------
    /// Suche in der Color-Tabelle nach den übergebenen Filterkriterien (SelectedFilters)
    /// Die gefundenen Datensätze werden nach ArticleId gruppiert
    /// Diese ArticleIds werden dann zurückgegeben
    /// </summary>
    /// <param name="filterList"></param>
    /// <returns></returns> 
    public async Task<List<ArticleCollection>> GetArticleCollectionModelsFromArticlesAsync(List<CatalogFilterEntryItem> filterList)
    {
        // ********************************************************************************************
        // Überprüfe zuerst, ob die übergebenen Filter den Bedingungen entsprechen
        // Wenn nicht, gebe eine leere Liste vom Typ ShoppingCartItem zurück

        // Wir haben nur einen einzigen Filter, und der ist NICHT vom Typ (ArticleID oder ColorID oder Single)
        //  Dann gebe eine leere Liste (ArticleIdModel) zurück
        //if (!filterList.Any(f => f.FilterType == CatalogFilterTypesEnum.ArticleID ||
        //                         f.FilterType == CatalogFilterTypesEnum.Contains01 ||
        //                         f.FilterType == CatalogFilterTypesEnum.Single01 ||
        //                         f.FilterType == CatalogFilterTypesEnum.Single02 ||
        //                         f.FilterType == CatalogFilterTypesEnum.Single03 ||
        //                         f.FilterType == CatalogFilterTypesEnum.Hierarchy01))
        //    return new List<ArticleCollectionModel>();

        // Wir haben zwei Filtereinträge und einer davon ist der InStock-Filter
        //if (filterList.Count == 2 && filterList.Any(f => f.FilterType == CatalogFilterTypesEnum.InStock))
        //    return new List<DBShoppingCartItem>();

        // Wir haben zwei Filtereinträge und sie sind vom Typ Label und Season (keine sonstigen Filter)
        //if (filterList.Count == 2 && filterList.Any(f => f.FilterType == CatalogFilterTypesEnum.Label) && filterList.Any(f => f.FilterType == CatalogFilterTypesEnum.Season))
        //    return new List<ArticleCollectionModel>();

        // Es ist ein Filter IsSticky enthalten, der ist aber true  
        //if (filterList.Any(f => f.FilterIsSticky == false) == false)
        //    return new List<ArticleCollectionModel>();
        // ********************************************************************************************

        await InitializeAsync();

        StringBuilder sbSelect = new();
        sbSelect.Append("SELECT ArticleId, ArticleNumber, HasStock ");
        sbSelect.Append("FROM Article ");
        sbSelect.Append("WHERE ");


        bool hasTrailingAND = false;

        foreach (CatalogFilterEntryItem f in filterList) // Es wurden Filter übergeben
        {
            switch (f.FilterType)
            {
                case CatalogFilterTypesEnum.Catalog: // Ein Filter mit LabelNumber in FilterTextContent ist vorhanden
                    sbSelect.Append("(LabelNumber = '" + f.FilterTextContent + "') AND ");
                    break;
                case CatalogFilterTypesEnum.Season: // Ein Filter mit SaisonNumber in FilterTextContent ist vorhanden
                    sbSelect.Append("(SeasonNumber = '" + f.FilterTextContent + "') AND ");
                    break;
                case CatalogFilterTypesEnum.Contains01:
                    sbSelect.Append("(ContainsFilter01 LIKE '%" + f.FilterTextContent + "%') AND ");
                    break;
                case CatalogFilterTypesEnum.Single01:
                    if (f.FilterTextContent != "*")
                        sbSelect.Append("(SingleFilter01 = '" + f.FilterTextContent + "') AND ");
                    break;
            }
            hasTrailingAND = true;
        }

        if (hasTrailingAND)
            sbSelect.Remove(sbSelect.Length - 4, 4);  // Entferne ggf. das letzte "AND" aus dem Select String
        else
            sbSelect.Remove(sbSelect.Length - 6, 6);  // Entferne ggf. das "WHERE" aus dem Select String

        // Wende den Selectstring an
        IEnumerable<ArticleCollection> sqliteResultlist = await Connection.QueryAsync<ArticleCollection>(sbSelect.ToString());

        return sqliteResultlist.ToList() ?? new List<ArticleCollection>();
    }

    #endregion

    #region Hierarchy, Single Filter and Others

    // -----------------------------------------------------------------------
    // Holt aus den Stammdaten eine kompakte Liste mit nur den Feldern,
    // die als Basis für unsere Filter benötigt werden.
    // Somit verkürzt sich die Ladezeit der Filter-ComboBoxen sehr
    // -----------------------------------------------------------------------
    // !! Wird nur beim Öffnen (NavigatedTo) der GridPage einmal ausgeführt !!
    // -----------------------------------------------------------------------


    public async Task<List<HierarchyAndSingleFilterResult>> GetFilterFieldslistAsync()
    {
        await InitializeAsync();


        StringBuilder sbSelect = new();
        sbSelect.Append("SELECT DISTINCT ");
        sbSelect.Append("SeasonNumber, ");
        sbSelect.Append("LabelNumber, ");
        sbSelect.Append("HierarchyFilter01, ");
        sbSelect.Append("HierarchyFilter02, ");
        sbSelect.Append("HierarchyFilter03, ");
        sbSelect.Append("HierarchyFilter04, ");
        sbSelect.Append("HierarchyFilter05, ");
        sbSelect.Append("SingleFilter01 ");
        sbSelect.Append("FROM Article ");

        IEnumerable<HierarchyAndSingleFilterResult> result = await Connection.QueryAsync<HierarchyAndSingleFilterResult>(sbSelect.ToString());

        return result.ToList() ?? new List<HierarchyAndSingleFilterResult>();
    }

    /// <summary>
    // -----------------------------------------------------------------------
    /// Wir benötigen diese Funktion für das Füllen der Vorschlagsliste der AutoSuggestBox
    // -----------------------------------------------------------------------
    public async Task<List<ContainsFilter01Result>> GetContainsFilter01ResultsAsync(List<CatalogFilterEntryItem> selectedFilters, string filterText)
    {
        if (string.IsNullOrEmpty(filterText))
            return new List<ContainsFilter01Result>();

        await InitializeAsync();

        StringBuilder sbSelect = new();
        sbSelect.Append("SELECT DISTINCT ");
        sbSelect.Append("ContainsFilter01, ");
        sbSelect.Append("ArticleNumber ");
        sbSelect.Append("FROM Article ");
        sbSelect.Append(string.Format("WHERE (ContainsFilter01 LIKE '%{0}%') AND ", filterText));

        foreach (CatalogFilterEntryItem f in selectedFilters.Where(w => w.FilterGroup == CatalogFilterGroupesEnum.PreFilter)) // Es wurden PreFilter (Saison oder Label) übergeben
        {
            switch (f.FilterType)
            {
                case CatalogFilterTypesEnum.Catalog: // Ein Filter mit LabelNumber in FilterTextContent ist vorhanden
                    sbSelect.Append("(LabelNumber = '" + f.FilterTextContent + "') AND ");
                    break;
                case CatalogFilterTypesEnum.Season: // Ein Filter mit SaisonNumber in FilterTextContent ist vorhanden
                    sbSelect.Append("(SeasonNumber = '" + f.FilterTextContent + "') AND ");
                    break;
            }
        }

        // Entferne das letzte "AND" aus dem Select String
        sbSelect.Remove(sbSelect.Length - 4, 4);

        sbSelect.Append("ORDER BY ArticleNumber ");
        sbSelect.Append("LIMIT 100 ");

        IEnumerable<ContainsFilter01Result> result = await Connection.QueryAsync<ContainsFilter01Result>(sbSelect.ToString());

        if (!result.Any())
            result.Append(new ContainsFilter01Result() { ArticleNumber = String.Empty, ContainsFilter01 = "Keine Ergebnisse gefunden" });

        return result.ToList() ?? new List<ContainsFilter01Result>();
    }

    public async Task<List<ContainsFilter01Result>> GetContainsFilter01ResultsAsync(List<CatalogFilterEntryItem> selectedFilters)
    {
        await InitializeAsync();

        StringBuilder sbSelect = new();
        sbSelect.Append("SELECT DISTINCT ");
        sbSelect.Append("ContainsFilter01, ");
        sbSelect.Append("ArticleNumber ");
        sbSelect.Append("FROM Article ");
        sbSelect.Append(string.Format("WHERE "));

        foreach (CatalogFilterEntryItem f in selectedFilters.Where(w => w.FilterGroup == CatalogFilterGroupesEnum.PreFilter)) // Es wurden PreFilter (Saison oder Label) übergeben
        {
            switch (f.FilterType)
            {
                case CatalogFilterTypesEnum.Catalog: // Ein Filter mit LabelNumber in FilterTextContent ist vorhanden
                    sbSelect.Append("(LabelNumber = '" + f.FilterTextContent + "') AND ");
                    break;
                case CatalogFilterTypesEnum.Season: // Ein Filter mit SaisonNumber in FilterTextContent ist vorhanden
                    sbSelect.Append("(SeasonNumber = '" + f.FilterTextContent + "') AND ");
                    break;
            }
        }

        // Entferne das letzte "AND" aus dem Select String
        sbSelect.Remove(sbSelect.Length - 4, 4);

        sbSelect.Append("ORDER BY ArticleNumber ");

        IEnumerable<ContainsFilter01Result> result = await Connection.QueryAsync<ContainsFilter01Result>(sbSelect.ToString());

        if (!result.Any())
            result.Append(new ContainsFilter01Result() { ArticleNumber = String.Empty, ContainsFilter01 = "Keine Ergebnisse gefunden" });

        return result.ToList() ?? new List<ContainsFilter01Result>();
    }


    /// <summary>
    // -----------------------------------------------------------------------
    /// ArtikelIds Suchen
    // -----------------------------------------------------------------------
    /// Suche in der Color-Tabelle nach den übergebenen Filterkriterien (SelectedFilters)
    /// Die gefundenen Datensätze werden nach ArticleId gruppiert
    /// Diese ArticleIds werden dann zurückgegeben
    /// </summary>
    /// <param name="filterList"></param>
    /// <returns></returns> 
    public async Task<List<ArticleCollection>> GetArticleCollectionAsync(List<CatalogFilterEntryItem> filterList)
    {
        // ********************************************************************************************
        // Überprüfe zuerst, ob die übergebenen Filter den Bedingungen entsprechen
        // Wenn nicht, gebe eine leere Liste vom Typ ShoppingCartItem zurück

        // Wir haben nur einen einzigen Filter, und der ist NICHT vom Typ (ArticleID oder ColorID oder Single)
        //  Dann gebe eine leere Liste (ArticleIdModel) zurück
        //if (!filterList.Any(f => f.FilterType == CatalogFilterTypesEnum.ArticleID ||
        //                         f.FilterType == CatalogFilterTypesEnum.Contains01 ||
        //                         f.FilterType == CatalogFilterTypesEnum.Single01 ||
        //                         f.FilterType == CatalogFilterTypesEnum.Single02 ||
        //                         f.FilterType == CatalogFilterTypesEnum.Single03 ||
        //                         f.FilterType == CatalogFilterTypesEnum.Hierarchy01))
        //    return new List<ArticleCollectionModel>();

        // Wir haben zwei Filtereinträge und einer davon ist der InStock-Filter
        //if (filterList.Count == 2 && filterList.Any(f => f.FilterType == CatalogFilterTypesEnum.InStock))
        //    return new List<DBShoppingCartItem>();

        // Wir haben zwei Filtereinträge und sie sind vom Typ Label und Season (keine sonstigen Filter)
        //if (filterList.Count == 2 && filterList.Any(f => f.FilterType == CatalogFilterTypesEnum.Label) && filterList.Any(f => f.FilterType == CatalogFilterTypesEnum.Season))
        //    return new List<ArticleCollectionModel>();

        // Es ist ein Filter IsSticky enthalten, der ist aber true  
        //if (filterList.Any(f => f.FilterIsSticky == false) == false)
        //    return new List<ArticleCollectionModel>();
        // ********************************************************************************************

        await InitializeAsync();

        StringBuilder sbSelect = new();
        sbSelect.Append("SELECT ArticleId, ArticleNumber, HasStock ");
        sbSelect.Append("FROM Article ");
        sbSelect.Append("WHERE ");

        bool hasTrailingAND = false;

        foreach (CatalogFilterEntryItem f in filterList) // Es wurden Filter übergeben
        {
            switch (f.FilterType)
            {
                case CatalogFilterTypesEnum.Catalog: // Ein Filter mit LabelNumber in FilterTextContent ist vorhanden
                    sbSelect.Append("(LabelNumber = '" + f.FilterTextContent + "') AND ");
                    break;
                case CatalogFilterTypesEnum.Season: // Ein Filter mit SaisonNumber in FilterTextContent ist vorhanden
                    sbSelect.Append("(SeasonNumber = '" + f.FilterTextContent + "') AND ");
                    break;
                case CatalogFilterTypesEnum.Contains01:
                    sbSelect.Append("(ContainsFilter01 LIKE '%" + f.FilterTextContent + "%') AND ");
                    break;
                case CatalogFilterTypesEnum.Single01:
                    if (f.FilterTextContent != "*")
                        sbSelect.Append("(SingleFilter01 = '" + f.FilterTextContent + "') AND ");
                    break;
            }
            hasTrailingAND = true;
        }

        if (hasTrailingAND)
            sbSelect.Remove(sbSelect.Length - 4, 4);  // Entferne ggf. das letzte "AND" aus dem Select String
        else
            sbSelect.Remove(sbSelect.Length - 6, 6);  // Entferne ggf. das "WHERE" aus dem Select String

        // Wende den Selectstring an
        IEnumerable<ArticleCollection> sqliteResultlist = await Connection.QueryAsync<ArticleCollection>(sbSelect.ToString());

        return sqliteResultlist.ToList() ?? new List<ArticleCollection>();
    }

    #endregion

}