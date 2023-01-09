namespace MyConveno.Toolkit.Sales4Pro.Client.Offline.BaseDataService;

/// <summary>
/// Artikel
/// </summary>
public record ArticleRecord : BaseModelRecord
{
    public ArticleRecord()
    {
        ArticleID = string.Empty;
        ArticleName = string.Empty;
        ArticleNumber = string.Empty;
        LabelNumber = string.Empty;
        SeasonNumber = string.Empty;
        ContainsFilter01 = string.Empty;
        SingleFilter01 = string.Empty;
        SingleFilter02 = string.Empty;
        SingleFilter03 = string.Empty;
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
    public string SingleFilter01 { get; init; }
    public string SingleFilter02 { get; init; }
    public string SingleFilter03 { get; init; }
    public string HierarchyFilter01 { get; init; }
    public string HierarchyFilter02 { get; init; }
    public string HierarchyFilter03 { get; init; }
    public string HierarchyFilter04 { get; init; }
    public string HierarchyFilter05 { get; init; }
    public bool HasStock { get; init; }
    public string Metadata { get; init; }

}
