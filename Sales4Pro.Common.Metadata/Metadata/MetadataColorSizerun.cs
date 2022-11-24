using System.Collections.Generic;

namespace Sales4Pro.Common.Metadata
{
    public class MetadataColorSizerun
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string PrintColor { get; set; }
        public string Size01 { get; set; }
        public string Size02 { get; set; }
        public string Size03 { get; set; }
        public string Size04 { get; set; }
        public string Size05 { get; set; }
        public string Size06 { get; set; }
        public string Size07 { get; set; }
        public string Size08 { get; set; }
        public string Size09 { get; set; }
        public string Size10 { get; set; }
        public string Size11 { get; set; }
        public string Size12 { get; set; }
        public string Size13 { get; set; }
        public string Size14 { get; set; }
        public string Size15 { get; set; }
        public string Size16 { get; set; }
        public string Size17 { get; set; }
        public string Size18 { get; set; }
        public string Size19 { get; set; }
        public string Size20 { get; set; }
        public string Size21 { get; set; }
        public string Size22 { get; set; }
        public string Size23 { get; set; }
        public string Size24 { get; set; }
        public string Size25 { get; set; }
        public string Size26 { get; set; }
        public string Size27 { get; set; }
        public string Size28 { get; set; }
        public string Size29 { get; set; }
        public string Size30 { get; set; }
        public string EAN01 { get; set; }
        public string EAN02 { get; set; }
        public string EAN03 { get; set; }
        public string EAN04 { get; set; }
        public string EAN05 { get; set; }
        public string EAN06 { get; set; }
        public string EAN07 { get; set; }
        public string EAN08 { get; set; }
        public string EAN09 { get; set; }
        public string EAN10 { get; set; }
        public string EAN11 { get; set; }
        public string EAN12 { get; set; }
        public string EAN13 { get; set; }
        public string EAN14 { get; set; }
        public string EAN15 { get; set; }
        public string EAN16 { get; set; }
        public string EAN17 { get; set; }
        public string EAN18 { get; set; }
        public string EAN19 { get; set; }
        public string EAN20 { get; set; }
        public string EAN21 { get; set; }
        public string EAN22 { get; set; }
        public string EAN23 { get; set; }
        public string EAN24 { get; set; }
        public string EAN25 { get; set; }
        public string EAN26 { get; set; }
        public string EAN27 { get; set; }
        public string EAN28 { get; set; }
        public string EAN29 { get; set; }
        public string EAN30 { get; set; }
        public int Stock01 { get; set; }
        public int Stock02 { get; set; }
        public int Stock03 { get; set; }
        public int Stock04 { get; set; }
        public int Stock05 { get; set; }
        public int Stock06 { get; set; }
        public int Stock07 { get; set; }
        public int Stock08 { get; set; }
        public int Stock09 { get; set; }
        public int Stock10 { get; set; }
        public int Stock11 { get; set; }
        public int Stock12 { get; set; }
        public int Stock13 { get; set; }
        public int Stock14 { get; set; }
        public int Stock15 { get; set; }
        public int Stock16 { get; set; }
        public int Stock17 { get; set; }
        public int Stock18 { get; set; }
        public int Stock19 { get; set; }
        public int Stock20 { get; set; }
        public int Stock21 { get; set; }
        public int Stock22 { get; set; }
        public int Stock23 { get; set; }
        public int Stock24 { get; set; }
        public int Stock25 { get; set; }
        public int Stock26 { get; set; }
        public int Stock27 { get; set; }
        public int Stock28 { get; set; }
        public int Stock29 { get; set; }
        public int Stock30 { get; set; }
        public List<MetadataColorPrice> Prices { get; set; }

        public MetadataColorSizerun()
        {
            Prices = new List<MetadataColorPrice>();
        }

        public void PasteData(MetadataColorSizerun item)
        {
            Number = item.Number;
            Name = item.Name;
            PrintColor = item.PrintColor;
            Size01 = item.Size01;
            Size02 = item.Size02;
            Size03 = item.Size03;
            Size04 = item.Size04;
            Size05 = item.Size05;
            Size06 = item.Size06;
            Size07 = item.Size07;
            Size08 = item.Size08;
            Size09 = item.Size09;
            Size10 = item.Size10;
            Size11 = item.Size11;
            Size12 = item.Size12;
            Size13 = item.Size13;
            Size14 = item.Size14;
            Size15 = item.Size15;
            Size16 = item.Size16;
            Size17 = item.Size17;
            Size18 = item.Size18;
            Size19 = item.Size19;
            Size20 = item.Size20;
            Size21 = item.Size21;
            Size22 = item.Size22;
            Size23 = item.Size23;
            Size24 = item.Size24;
            Size25 = item.Size25;
            Size26 = item.Size26;
            Size27 = item.Size27;
            Size28 = item.Size28;
            Size29 = item.Size29;
            Size30 = item.Size30;

            EAN01 = item.EAN01;
            EAN02 = item.EAN02;
            EAN03 = item.EAN03;
            EAN04 = item.EAN04;
            EAN05 = item.EAN05;
            EAN06 = item.EAN06;
            EAN07 = item.EAN07;
            EAN08 = item.EAN08;
            EAN09 = item.EAN09;
            EAN10 = item.EAN10;
            EAN11 = item.EAN11;
            EAN12 = item.EAN12;
            EAN13 = item.EAN13;
            EAN14 = item.EAN14;
            EAN15 = item.EAN15;
            EAN16 = item.EAN16;
            EAN17 = item.EAN17;
            EAN18 = item.EAN18;
            EAN19 = item.EAN19;
            EAN20 = item.EAN20;
            EAN21 = item.EAN21;
            EAN22 = item.EAN22;
            EAN23 = item.EAN23;
            EAN24 = item.EAN24;
            EAN25 = item.EAN25;
            EAN26 = item.EAN26;
            EAN27 = item.EAN27;
            EAN28 = item.EAN28;
            EAN29 = item.EAN29;
            EAN30 = item.EAN30;

            Stock01 = item.Stock01;
            Stock02 = item.Stock02;
            Stock03 = item.Stock03;
            Stock04 = item.Stock04;
            Stock05 = item.Stock05;
            Stock06 = item.Stock06;
            Stock07 = item.Stock07;
            Stock08 = item.Stock08;
            Stock09 = item.Stock09;
            Stock10 = item.Stock10;
            Stock11 = item.Stock11;
            Stock12 = item.Stock12;
            Stock13 = item.Stock13;
            Stock14 = item.Stock14;
            Stock15 = item.Stock15;
            Stock16 = item.Stock16;
            Stock17 = item.Stock17;
            Stock18 = item.Stock18;
            Stock19 = item.Stock19;
            Stock20 = item.Stock20;
            Stock21 = item.Stock21;
            Stock22 = item.Stock22;
            Stock23 = item.Stock23;
            Stock24 = item.Stock24;
            Stock25 = item.Stock25;
            Stock26 = item.Stock26;
            Stock27 = item.Stock27;
            Stock28 = item.Stock28;
            Stock29 = item.Stock29;
            Stock30 = item.Stock30;
        }

    }
}