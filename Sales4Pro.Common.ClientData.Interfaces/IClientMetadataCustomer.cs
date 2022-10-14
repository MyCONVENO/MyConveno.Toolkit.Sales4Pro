namespace MyConveno.Toolkit.Sales4Pro.Common.ClientData.Interfaces;

public interface IClientMetadataCustomer
{
    bool IsCustomerAreaFilterVisible { get; set; }
    bool IsCustomerAreaSearchDefault { get; set; }
    bool IsCustomerFavoritesSearchDefault { get; set; }
    bool IsCustomergroupFilterVisible { get; set; }
}