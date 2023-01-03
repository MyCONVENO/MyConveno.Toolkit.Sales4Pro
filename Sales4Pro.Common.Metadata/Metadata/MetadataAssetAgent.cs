using System.Collections.Generic;

namespace Sales4Pro.Common.Metadata
{
    public class MetadataAssetAgent
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string ConfirmFax { get; set; }
        public string ConfirmEmail { get; set; }
        public string Remark { get; set; }
        public decimal Commission { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string City { get; set; }
        public string ZIP { get; set; }
        public string Street { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string EMail { get; set; }
        public string Mobile { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string PricelistID { get; set; }
        public string CollectionFilter { get; set; }
        public string DefaultPaymentTermText { get; set; }
        public List<MetadataAssetCatalog> Catalogs { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, Number);
        }
    }

}