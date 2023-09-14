using Microsoft.Datasync.Client.SQLiteStore;

namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService
{
    public interface IAzureSyncService
    {
        string AzureURL { get; init; }
        OfflineSQLiteStore SQLiteStore { get; init; }

        Task<bool> DeleteCustomerFavoriteAsync(string userId, string customerNumber);
        Task<bool> DeleteCustomerNoteAsync(string id);
        Task<bool> DeleteShoppingCartAsync(string id);
        Task<SyncCustomerFavorite> GetCustomerFavoriteAsync(string id);
        Task<List<SyncCustomerFavorite>> GetCustomerFavoritesAsync(string userId);
        Task<SyncCustomerNote> GetCustomerNoteAsync(string id);
        Task<List<SyncCustomerNote>> GetCustomerNotesAsync(string customerNumber);
        Task<SyncShoppingCart> GetShoppingCartAsync(string id);
        Task<int> GetShoppingCartsCountAsync();
        Task<List<SyncShoppingCart>> GetShoppingCartsAsync();
        Task InitializeAsync();
        Task<string> SaveCustomerFavoriteAsync(SyncCustomerFavorite upsertItem);
        Task<string> SaveCustomerNoteAsync(SyncCustomerNote upsertItem);
        Task<string> SaveShoppingCartAsync(SyncShoppingCart upsertItem);
        Task<bool> Synchronize(string userId, bool pullTables);
    }
}