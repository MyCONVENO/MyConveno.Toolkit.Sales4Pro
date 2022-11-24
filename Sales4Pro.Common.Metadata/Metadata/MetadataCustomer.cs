using System.Collections.Generic;

namespace Sales4Pro.Common.Metadata
{
    public class MetadataCustomer
    {
        public MetadataCustomerCore Core { get; set; }
        public List<MetadataCustomerContactPerson> ContactPersons { get; set; }
        public List<MetadataCustomerDeliveryAddress> DeliveryAddresses { get; set; }
        public List<MetadataCustomerInvoiceAddress> InvoiceAddresses { get; set; }
        public List<MetadataCustomerSpecialDiscount> SpecialDiscounts { get; set; }
        public List<MetadataCustomerArticleDiscount> ArticleDiscounts { get; set; }

        public MetadataCustomer()
        {
            Core = new MetadataCustomerCore();
            DeliveryAddresses = new List<MetadataCustomerDeliveryAddress>();
            InvoiceAddresses = new List<MetadataCustomerInvoiceAddress>();
            ContactPersons = new List<MetadataCustomerContactPerson>();
            SpecialDiscounts = new List<MetadataCustomerSpecialDiscount>();
            ArticleDiscounts = new List<MetadataCustomerArticleDiscount>();
        }

    }
}