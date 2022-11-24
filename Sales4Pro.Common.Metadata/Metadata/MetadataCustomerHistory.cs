using System;

namespace Sales4Pro.Common.Metadata
{
    public class MetadataCustomerHistory
    {
        public string Season { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Ordernumber { get; set; }
        public string Ordertype { get; set; }
        public string Art { get; set; }
        public string Col { get; set; }
        public string ColName { get; set; }
        public int InvQty { get; set; }
        public double InvVal { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Art, Col);
        }
    }
}