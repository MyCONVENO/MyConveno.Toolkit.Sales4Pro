namespace MyConveno.Toolkit.Sales4Pro.Client.Offline.BaseDataService;

public class Color
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

    public string ColorID { get; set; }
    public string SeasonNumber { get; set; }
    public string LabelNumber { get; set; }
    public string Category { get; set; }
    public string ArticleNumber { get; set; }
    public string ColorNumber { get; set; }
    public string ColorName { get; set; }
    public bool StockArticle { get; set; }
    public bool HasImage { get; set; }
    public string Metadata { get; set; }
}