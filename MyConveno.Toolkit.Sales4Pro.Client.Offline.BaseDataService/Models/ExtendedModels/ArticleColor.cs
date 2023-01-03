namespace MyConveno.Toolkit.Sales4Pro.Client.Offline.BaseDataService;

public class ArticleColor
{
    public ArticleColor()
    {
        ArticleID = string.Empty;
        ArticleNumber = string.Empty;
        ArticleName = string.Empty;
        LabelNumber = string.Empty;
        SeasonNumber = string.Empty;
        StockArticle = false;
        ArticleMetadataJSON = string.Empty;
        ColorID = string.Empty;
        ColorNumber = string.Empty;
        ColorName = string.Empty;
        Category = string.Empty;
        ColorMetadataJSON = string.Empty;
    }

    public string ArticleID { get; set; }
    public string ArticleNumber { get; set; }
    public string ArticleName { get; set; }
    public string LabelNumber { get; set; }
    public string SeasonNumber { get; set; }
    public bool StockArticle { get; set; }
    public string ArticleMetadataJSON { get; set; }
    public string ColorID { get; set; }
    public string ColorNumber { get; set; }
    public string ColorName { get; set; }
    public string Category { get; set; }
    public string ColorMetadataJSON { get; set; }

}
