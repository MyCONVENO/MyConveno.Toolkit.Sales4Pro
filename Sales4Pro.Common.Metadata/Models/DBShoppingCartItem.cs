namespace Sales4Pro.Common.Metadata.Models
{
    public class DBShoppingCartItem
    {
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
}