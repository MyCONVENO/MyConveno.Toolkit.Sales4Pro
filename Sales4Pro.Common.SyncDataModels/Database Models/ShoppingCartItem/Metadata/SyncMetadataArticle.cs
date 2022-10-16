using MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels;

public class SyncMetadataArticle : ISyncMetadataArticle
{
    public string ArticleID { get; set; }
    public string ArticleNumber { get; set; }
    public string ArticleName { get; set; }
    public string LabelNumber { get; set; }
    public string LabelName { get; set; }
    public string SeasonNumber { get; set; }
    public string SeasonName { get; set; }
    public string SeasonLongName { get; set; }
    public string LabelLongName { get; set; }
    public string FormName { get; set; }
    public string FormNumber { get; set; }
    public string ProductAreaNumber { get; set; }
    public string Material { get; set; }
    public string InfoText1 { get; set; }
    public string InfoText2 { get; set; }
    public string Image { get; set; }
}