using System.Collections.Generic;

namespace Sales4Pro.Common.Metadata
{
    public class MetadataArticle
    {
        public MetadataArticleCore Core { get; set; }
        public List<MetadataArticleProductImage> ColorImages { get; set; }
        public List<MetadataArticlePrice> Prices { get; set; }

        public MetadataArticle()
        {
            Core = new MetadataArticleCore();
            ColorImages = new List<MetadataArticleProductImage>();
        }

    }
}