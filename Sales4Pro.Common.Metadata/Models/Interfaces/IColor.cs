namespace Sales4Pro.Common.Metadata.Interfaces
{
    public interface IColor : IBaseModel
    {
        string ColorID { get; set; }
        string SeasonNumber { get; set; }
        string LabelNumber { get; set; }
        string ArticleNumber { get; set; }
        string ColorNumber { get; set; }
        string ColorName { get; set; }
        bool StockArticle { get; set; }
        bool HasImage { get; set; }
        string Metadata { get; set; }
        MetadataColor MetadataColor { get; set; }
    }
}
