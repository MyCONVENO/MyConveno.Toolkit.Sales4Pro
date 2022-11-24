using Newtonsoft.Json;
using Sales4Pro.Common.Metadata.Interfaces;
using SQLite;

namespace Sales4Pro.Common.Metadata.Models
{
    public class Color : BaseModel, IColor
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

            MetadataColor = new MetadataColor();
        }

        [PrimaryKey]
        public string ColorID { get; set; }

        [Indexed]
        public string SeasonNumber { get; set; }

        [Indexed]
        public string LabelNumber { get; set; }
        
        [Indexed]
        public string Category { get; set; }
        
        [Indexed]
        public string ArticleNumber { get; set; }
        public string ColorNumber { get; set; }
        public string ColorName { get; set; }
        public bool StockArticle { get; set; }
        public bool HasImage { get; set; }
        public string Metadata { get; set; }
      
        [Ignore]
        public MetadataColor MetadataColor { get; set; }

        public void DeserializeMetadata()
        {
            MetadataColor = JsonConvert.DeserializeObject<MetadataColor>(Metadata);
        }

    }
}
