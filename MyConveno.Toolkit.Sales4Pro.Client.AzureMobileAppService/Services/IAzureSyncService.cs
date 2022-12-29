using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService;

public interface IAzureSyncService
{
    Task<bool> ComputeCustomerIsFavoriteAsync(string userID, string customerNumber);
    Task<bool> DeleteCustomerFavoriteAsync(string userId, string customerNumber);
    Task DeleteCustomerNoteAsync(SyncCustomerNote syncCustomerNote);
    Task<bool> DeleteShoppingCartAsync(SyncShoppingCart syncShoppingCart);
    Task<bool> DeleteShoppingCartItemAsync(SyncShoppingCartItem syncShoppingCartItem);
    Task<List<SyncCustomerFavorite>> GetCustomerFavoritesAsync(string userId);
    Task<List<SyncCustomerNote>> GetCustomerNotesAsync(string customerNumber);
    ////////Task<List<SyncShoppingCart>> GetDeserializedShoppingCartsAsync(List<ShoppingCartsFilterEntryItem> filterList = null);
    //string GetExportFileName(SyncShoppingCart cart, int itemsCount);
    Task<int> GetPendingOrdersCountAsync();
    Task<List<SyncShoppingCart>> GetShoppingCartsAsync();
    Task<SyncShoppingCart> GetShoppingCartAsync(string shoppingCartID);
    Task<int> GetShoppingCartCountAsync();
    ////////Task<MemoryStream> GetShoppingCartItemCatalogPDFAsync(List<ShoppingCartsFilterEntryItem> filterList, string pricelistID);
    Task<List<SyncShoppingCartItem>> GetShoppingCartItemsAsync(string shoppingCartID, string searchParameter, bool specialSort = false);
    Task<int> GetShoppingCartItemsCountAsync(string shoppingCartID);
    Task InitializeAsync();
    Task ResetShoppingCartPricesAsync(string ShoppingCartID);
    Task<string> SaveCustomerFavoriteAsync(SyncCustomerFavorite syncCustomerFavorite);
    Task<string> SaveCustomerNoteAsync(SyncCustomerNote syncCustomerNote);
    Task<string> SaveShoppingCartAsync(SyncShoppingCart syncShoppingCart);
    Task<string> SaveShoppingCartItemAsync(SyncShoppingCartItem syncShoppingCartItem);
    Task<bool> SyncAllTablesAsync(string userId, bool pullTables);
    Task<bool> Synchronize(string userid, bool pullTables = true);
}