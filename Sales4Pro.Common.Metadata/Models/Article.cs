﻿using Newtonsoft.Json;
using Sales4Pro.Common.Metadata.Interfaces;
using SQLite;

namespace Sales4Pro.Common.Metadata.Models
{
    /// <summary>
    /// Artikel
    /// </summary>
    public class Article : BaseModel, IArticle
    {
        public Article()
        {
            ArticleID = string.Empty;
            ArticleName = string.Empty;
            ArticleNumber = string.Empty;
            LabelNumber = string.Empty;
            SeasonNumber = string.Empty;
            ContainsFilter01 = string.Empty;
            SingleFilter01 = string.Empty;
            SingleFilter02 = string.Empty;
            SingleFilter03 = string.Empty;
            HierarchyFilter01 = string.Empty;
            HierarchyFilter02 = string.Empty;
            HierarchyFilter03 = string.Empty;
            HierarchyFilter04 = string.Empty;
            HierarchyFilter05 = string.Empty;
            HasStock = false;
            Metadata = string.Empty;

            MetadataArticle = new MetadataArticle();
        }

        [PrimaryKey]
        public string ArticleID { get; set; }
        [Indexed]
        public string ArticleNumber { get; set; }
        [Indexed]
        public string ArticleName { get; set; }
        public string LabelNumber { get; set; }
        [Indexed]
        public string SeasonNumber { get; set; }
        [Indexed]
        public string ContainsFilter01 { get; set; }
        public string SingleFilter01 { get; set; }
        public string SingleFilter02 { get; set; }
        public string SingleFilter03 { get; set; }
        public string HierarchyFilter01 { get; set; }
        public string HierarchyFilter02 { get; set; }
        public string HierarchyFilter03 { get; set; }
        public string HierarchyFilter04 { get; set; }
        public string HierarchyFilter05 { get; set; }
        [Indexed]
        public bool HasStock { get; set; }
        public string Metadata { get; set; }

        [Ignore]
        public MetadataArticle MetadataArticle { get; set; }

        public void DeserializeMetadata()
        {
            MetadataArticle = JsonConvert.DeserializeObject<MetadataArticle>(Metadata);
        }
    }
}