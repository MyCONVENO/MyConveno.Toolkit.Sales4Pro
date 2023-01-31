namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public interface IMetadataClientContent
{
    string AccentColorDark1String { get; set; }
    string AccentColorDark2String { get; set; }
    string AccentColorDark3String { get; set; }
    string AccentColorLight1String { get; set; }
    string AccentColorLight2String { get; set; }
    string AccentColorLight3String { get; set; }
    string AccentColorString { get; set; }
    MetadataClientContent.ClientMetadataApp App { get; set; }
    MetadataClientContent.ClientMetadataCatalog Catalog { get; set; }
    MetadataClientContent.ClientMetadataCustomer Customer { get; set; }
    string ImagePathString { get; set; }
    MetadataClientContent.ClientMetadataPageProperties PageProperties { get; set; }
    MetadataClientContent.ClientMetadataReports Reports { get; set; }
    MetadataClientContent.ClientMetadataShoppingCart ShoppingCart { get; set; }
    MetadataClientContent.ClientMetadataShoppingCartItem ShoppingCartItem { get; set; }
    string SupportPersonEmail { get; set; }
    string SupportPersonName { get; set; }
    string SupportPersonPhone { get; set; }
}