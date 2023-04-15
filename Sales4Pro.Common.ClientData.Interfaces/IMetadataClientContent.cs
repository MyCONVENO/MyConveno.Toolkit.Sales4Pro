namespace MyConveno.Toolkit.Sales4Pro.Common.ClientData.Interfaces;

public interface IMetadataClientContent
{
    string AccentColorString { get; set; }
    IClientMetadataCatalog Catalog { get; set; }
    IClientMetadataCustomer Customer { get; set; }
    string ImagePathString { get; set; }
    IClientMetadataPageProperties PageProperties { get; set; }
    IClientMetadataReports Reports { get; set; }
    IClientMetadataShoppingCart ShoppingCart { get; set; }
    IClientMetadataShoppingCartItem ShoppingCartItem { get; set; }
    string SupportPersonEmail { get; set; }
    string SupportPersonName { get; set; }
    string SupportPersonPhone { get; set; }
}