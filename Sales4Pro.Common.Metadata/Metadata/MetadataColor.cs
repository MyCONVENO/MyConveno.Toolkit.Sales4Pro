using System.Collections.Generic;

namespace Sales4Pro.Common.Metadata
{
    public class MetadataColor
    {
        public MetadataColorCore Core { get; set; }
        public List<MetadataColorSizerun> Sizeruns { get; set; }

        public MetadataColor()
        {
            Core = new MetadataColorCore();
            Sizeruns = new List<MetadataColorSizerun>();
        }

    }
}