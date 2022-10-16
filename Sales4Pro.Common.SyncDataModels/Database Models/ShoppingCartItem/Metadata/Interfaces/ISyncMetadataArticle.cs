namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;

public interface ISyncMetadataArticle
{
    string ArticleID { get; set; }
    string ArticleName { get; set; }
    string ArticleNumber { get; set; }
    string FormName { get; set; }
    string FormNumber { get; set; }
    string ProductAreaNumber { get; set; }
    string Image { get; set; }
    string InfoText1 { get; set; }
    string InfoText2 { get; set; }
    string LabelLongName { get; set; }
    string LabelName { get; set; }
    string LabelNumber { get; set; }
    string Material { get; set; }
    string SeasonLongName { get; set; }
    string SeasonName { get; set; }
    string SeasonNumber { get; set; }

    //void Initialize(SyncShoppingCartItem syncShoppingCartItem);

}
