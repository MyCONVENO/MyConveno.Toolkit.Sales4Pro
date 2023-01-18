namespace MyConveno.Toolkit.Sales4Pro.Client.Offline.BaseDataService;

/// <summary>
/// Artikel
/// </summary>
public class Article : BaseModel
{
    public Article()
    {
        ArticleID = string.Empty;
        ArticleName = string.Empty;
        ArticleNumber = string.Empty;
        LabelNumber = string.Empty;
        SeasonNumber = string.Empty;
        ContainsFilter01 = string.Empty;
        StartsWithFilter01 = string.Empty;
        StartsWithFilter02 = string.Empty;
        StartsWithFilter03 = string.Empty;
        HierarchyFilter01 = string.Empty;
        HierarchyFilter02 = string.Empty;
        HierarchyFilter03 = string.Empty;
        HierarchyFilter04 = string.Empty;
        HierarchyFilter05 = string.Empty;
        HasStock = false;
        Metadata = string.Empty;
    }

    public string ArticleID { get; init; }
    public string ArticleNumber { get; init; }
    public string ArticleName { get; init; }
    public string LabelNumber { get; init; }
    public string SeasonNumber { get; init; }
    public string ContainsFilter01 { get; init; }
    public string StartsWithFilter01 { get; init; }
    public string StartsWithFilter02 { get; init; }
    public string StartsWithFilter03 { get; init; }
    public string HierarchyFilter01 { get; init; }
    public string HierarchyFilter02 { get; init; }
    public string HierarchyFilter03 { get; init; }
    public string HierarchyFilter04 { get; init; }
    public string HierarchyFilter05 { get; init; }
    public bool HasStock { get; init; }
    public string Metadata { get; init; }

}
