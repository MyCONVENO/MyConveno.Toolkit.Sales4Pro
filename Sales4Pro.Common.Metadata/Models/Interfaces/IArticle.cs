namespace Sales4Pro.Common.Metadata.Interfaces
{
    public interface IArticle : IBaseModel
    {
        string ArticleID { get; set; }
        string LabelNumber { get; set; }
        string SeasonNumber { get; set; }
        string ArticleName { get; set; }
        string ArticleNumber { get; set; }
        bool HasStock { get; set; }
        string ContainsFilter01 { get; set; }
        string HierarchyFilter01 { get; set; }
        string HierarchyFilter02 { get; set; }
        string HierarchyFilter03 { get; set; }
        string HierarchyFilter04 { get; set; }
        string HierarchyFilter05 { get; set; }
        string SingleFilter01 { get; set; }
        string SingleFilter02 { get; set; }
        string SingleFilter03 { get; set; }
        string Metadata { get; set; }

        MetadataArticle MetadataArticle { get; set; }
    }
}
