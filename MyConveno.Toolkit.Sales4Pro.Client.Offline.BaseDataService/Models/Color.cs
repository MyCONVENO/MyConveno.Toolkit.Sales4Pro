namespace MyConveno.Toolkit.Sales4Pro.Client.Offline.BaseDataService;

public record Color : BaseModel
{
    public Color()
    {
        ColorID = string.Empty;
        SeasonNumber = string.Empty;
        LabelNumber = string.Empty;
        Category = string.Empty;
        ArticleNumber = string.Empty;
        ColorNumber = string.Empty;
        ColorName = string.Empty;
        StockArticle = false;
        HasImage = false;
        Metadata = string.Empty;
    }

    public string ColorID { get; init; }
    public string SeasonNumber { get; init; }
    public string LabelNumber { get; init; }
    public string Category { get; init; }
    public string ArticleNumber { get; init; }
    public string ColorNumber { get; init; }
    public string ColorName { get; init; }
    public bool StockArticle { get; init; }
    public bool HasImage { get; init; }
    public string Metadata { get; init; }
}