using Microsoft.Datasync.Client;
using Microsoft.Datasync.Client.Offline;
using Microsoft.Datasync.Client.Offline.Queue;
using Microsoft.Datasync.Client.SQLiteStore;
using System.Collections.ObjectModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService;

public class AzureSyncService : IAzureSyncService
{
    private DatasyncClient? _client;

    private IOfflineTable<SyncCustomerFavorite>? syncCustomerFavoriteTable;
    private IOfflineTable<SyncCustomerNote>? syncCustomerNoteTable;
    private IOfflineTable<SyncShoppingCart>? syncShoppingCartTable;
    private IOfflineTable<SyncShoppingCartItem>? syncShoppingCartItemTable;

    public AzureSyncService(string url, OfflineSQLiteStore sqliteStore)
    {
        AzureURL = url;
        SQLiteStore = sqliteStore;
    }

    /// <summary>
    /// When set to true, the client and table and both initialized.
    /// </summary>
    private bool _initialized = false;

    private bool _syncIsRunning = false;

    /// <summary>
    /// Used for locking the initialization block to ensure only one initialization happens.
    /// </summary>
    private readonly SemaphoreSlim _asyncLock = new(1, 1);

    /// <summary>
    /// When using authentication, the token requestor to use.
    /// </summary>
    public Func<Task<AuthenticationToken>>? TokenRequestor;

    public string AzureURL { get; init; }
    public OfflineSQLiteStore SQLiteStore { get; init; }

    #region Commands

    private void UpdatePendingOperationDisplay()
    {
        // ****************************************************************************
        // Update Pending Operations
        // ****************************************************************************
        long? pendingOperations = _client == null ? 0 : _client.PendingOperations;
        //WeakReferenceMessenger.Default.Send(new PendingOperationsChanged(pendingOperations));
        // ****************************************************************************
    }

    #endregion

    #region Tasks

    public async Task InitializeAsync()
    {
        // Short circuit, in case we are already initialized.
        if (_initialized)
        {
            return;
        }

        // Wait to get the async initialization lock
        await _asyncLock.WaitAsync();
        if (_initialized)
        {
            // This will also execute the async lock.
            return;
        }

        // Create the offline store definition




        //string connectionString = new UriBuilder { Scheme = "file", Path = LocalSyncDBName}.Uri.ToString();
        //string connectionString = new UriBuilder { Scheme = "file", Path = LocalSyncDBName, Query = "?mode=rwc" }.Uri.ToString();
        //var store = new OfflineSQLiteStore(connectionString);

        //store.DefineTable<TodoItem>();
        SQLiteStore.DefineTable<SyncCustomerFavorite>();
        SQLiteStore.DefineTable<SyncCustomerNote>();
        SQLiteStore.DefineTable<SyncShoppingCart>();
        SQLiteStore.DefineTable<SyncShoppingCartItem>();

        var options = new DatasyncClientOptions
        {
            OfflineStore = SQLiteStore
        };

        // Create the datasync client.
        _client = TokenRequestor == null
            ? new DatasyncClient(AzureURL, options)
            : new DatasyncClient(AzureURL, new GenericAuthenticationProvider(TokenRequestor), options);

        // Initialize the database
        await _client.InitializeOfflineStoreAsync();

        // Get a reference to the offline tables.
        //_table = _client.GetOfflineTable<TodoItem>();
        syncCustomerFavoriteTable = _client.GetOfflineTable<SyncCustomerFavorite>();
        syncCustomerNoteTable = _client.GetOfflineTable<SyncCustomerNote>();
        syncShoppingCartTable = _client.GetOfflineTable<SyncShoppingCart>();
        syncShoppingCartItemTable = _client.GetOfflineTable<SyncShoppingCartItem>();

        // Set _initialized to true to prevent duplication of locking.
        _initialized = true;

        _asyncLock.Release();
    }

    public async Task<bool> Synchronize(string userId, bool pullTables)
    {
        if (_syncIsRunning)
        { return false; }

        _syncIsRunning = true;

        UpdatePendingOperationDisplay();
        ReadOnlyCollection<TableOperationError>? syncErrors = null;

        try
        {
            await InitializeAsync();

            //////await Task.Delay(1000);

            if (syncCustomerNoteTable is not null)
                await syncCustomerNoteTable.PushItemsAsync();

            if (syncCustomerFavoriteTable is not null)
                await syncCustomerFavoriteTable.PushItemsAsync();

            if (syncShoppingCartTable is not null)
                await syncShoppingCartTable.PushItemsAsync();

            if (syncShoppingCartItemTable is not null)
                await syncShoppingCartItemTable.PushItemsAsync();

            //////await Task.Delay(1000);

            if (pullTables && (string.IsNullOrEmpty(userId) == false))
            {
                if (syncCustomerNoteTable is not null)
                    await syncCustomerNoteTable.PullItemsAsync();

                if (syncCustomerFavoriteTable is not null)
                    await syncCustomerFavoriteTable.PullItemsAsync(syncCustomerFavoriteTable.CreateQuery().Where(w => w.UserID == userId));

                if (syncShoppingCartTable is not null)
                    await syncShoppingCartTable.PullItemsAsync(syncShoppingCartTable.CreateQuery().Where(w => w.UserID == userId && w.StatusID == 10));

                if (syncShoppingCartItemTable is not null)
                    await syncShoppingCartItemTable.PullItemsAsync(syncShoppingCartItemTable.CreateQuery().Where(w => w.UserID == userId));
            }
            else
            {
                _syncIsRunning = false;
                return false;
            }
            UpdatePendingOperationDisplay();
        }
        catch (PushFailedException exc)
        {
            if (exc?.PushResult != null)
            { syncErrors = (ReadOnlyCollection<TableOperationError>?)exc.PushResult.Errors; }
        }

        _syncIsRunning = false;
        return true;
    }

    #endregion



    #region CustomerFavorites

    #region Base CRUD

    public async Task<string> SaveCustomerFavoriteAsync(SyncCustomerFavorite upsertItem)
    {
        await InitializeAsync();

        syncCustomerFavoriteTable = _client?.GetOfflineTable<SyncCustomerFavorite>();

        if (syncCustomerFavoriteTable is null) return string.Empty;

        if (upsertItem.Id is null)
            await syncCustomerFavoriteTable.InsertItemAsync(upsertItem);
        else
            await syncCustomerFavoriteTable.ReplaceItemAsync(upsertItem);

        return upsertItem.Id is not null ? upsertItem.Id.ToString() : string.Empty;
    }

    public async Task<bool> DeleteCustomerFavoriteAsync(string userId, string customerNumber)
    {
        await InitializeAsync();
        syncCustomerFavoriteTable = _client?.GetOfflineTable<SyncCustomerFavorite>();

        if (syncCustomerFavoriteTable is null) return false;

        SyncCustomerFavorite? itemToDelete = await syncCustomerFavoriteTable.Where(w => w.UserID == userId &&
                                                                                        w.CustomerNumber == customerNumber)
                                                                            .ToAsyncEnumerable()
                                                                            .FirstOrDefaultAsync();

        if (itemToDelete is null) return false;

        await syncCustomerFavoriteTable.DeleteItemAsync(itemToDelete);
        return true;
    }

    public async Task<SyncCustomerFavorite> GetCustomerFavoriteAsync(string id)
    {
        await InitializeAsync();
        syncCustomerFavoriteTable = _client?.GetOfflineTable<SyncCustomerFavorite>();

        if (syncCustomerFavoriteTable is null) return new SyncCustomerFavorite();

        SyncCustomerFavorite? syncCustomerFavorite = await syncCustomerFavoriteTable.GetItemAsync(id);

        return syncCustomerFavorite is not null ? syncCustomerFavorite : new SyncCustomerFavorite();
    }

    #endregion

    #region Get Data

    public async Task<List<SyncCustomerFavorite>> GetCustomerFavoritesAsync(string userId)
    {
        await InitializeAsync();

        syncCustomerFavoriteTable = _client?.GetOfflineTable<SyncCustomerFavorite>();

        if (syncCustomerFavoriteTable is null) return new List<SyncCustomerFavorite>();

        List<SyncCustomerFavorite> syncCustomerFavorites = await syncCustomerFavoriteTable.Where(w => w.UserID == userId)
                                                                                          .IncludeTotalCount()
                                                                                          .ToAsyncEnumerable()
                                                                                          .ToListAsync();
        return syncCustomerFavorites;
    }

    #endregion

    #endregion

    #region CustomerNotes

    #region Base CRUD

    public async Task<string> SaveCustomerNoteAsync(SyncCustomerNote upsertItem)
    {
        await InitializeAsync();
        syncCustomerNoteTable = _client?.GetOfflineTable<SyncCustomerNote>();

        if (syncCustomerNoteTable is null) return string.Empty;

        if (upsertItem.Id is null)
            await syncCustomerNoteTable.InsertItemAsync(upsertItem);
        else
            await syncCustomerNoteTable.ReplaceItemAsync(upsertItem);

        return upsertItem.Id is not null ? upsertItem.Id.ToString() : string.Empty;
    }

    public async Task<bool> DeleteCustomerNoteAsync(string id)
    {
        await InitializeAsync();
        syncCustomerNoteTable = _client?.GetOfflineTable<SyncCustomerNote>();

        if (syncCustomerNoteTable is null) return false;

        SyncCustomerNote? itemToDelete = await syncCustomerNoteTable.Where(w => w.Id == id)
                                                                    .ToAsyncEnumerable()
                                                                    .FirstOrDefaultAsync();

        if (itemToDelete is null) return false;

        await syncCustomerNoteTable.DeleteItemAsync(itemToDelete);
        return true;

    }

    public async Task<SyncCustomerNote> GetCustomerNoteAsync(string id)
    {
        await InitializeAsync();
        syncCustomerNoteTable = _client?.GetOfflineTable<SyncCustomerNote>();

        if (syncCustomerNoteTable is null) return new SyncCustomerNote();

        SyncCustomerNote? syncCustomerNote = await syncCustomerNoteTable.GetItemAsync(id);

        return syncCustomerNote is not null ? syncCustomerNote : new SyncCustomerNote();
    }

    #endregion

    #region Get Data

    public async Task<List<SyncCustomerNote>> GetCustomerNotesAsync(string customerNumber)
    {
        await InitializeAsync();
        syncCustomerNoteTable = _client?.GetOfflineTable<SyncCustomerNote>();

        if (syncCustomerNoteTable is null) return new List<SyncCustomerNote>();

        List<SyncCustomerNote> syncCustomerNotes = await syncCustomerNoteTable.Where(w => w.CustomerNumber == customerNumber)
                                                                              .OrderBy(o => o.NoteCreated)
                                                                              .IncludeTotalCount()
                                                                              .ToAsyncEnumerable()
                                                                              .ToListAsync();

        return syncCustomerNotes is not null ? syncCustomerNotes : new List<SyncCustomerNote>();
    }

    #endregion

    #endregion

    #region ShoppingCarts

    #region Base CRUD

    public async Task<string> SaveShoppingCartAsync(SyncShoppingCart upsertItem)
    {
        await InitializeAsync();
        syncShoppingCartTable = _client?.GetOfflineTable<SyncShoppingCart>();

        if (syncShoppingCartTable is null) return string.Empty;

        if (upsertItem.Id is null)
            await syncShoppingCartTable.InsertItemAsync(upsertItem);
        else
            await syncShoppingCartTable.ReplaceItemAsync(upsertItem);

        return upsertItem.Id is not null ? upsertItem.Id.ToString() : string.Empty;
    }

    public async Task<bool> DeleteShoppingCartAsync(string id)
    {
        await InitializeAsync();
        syncShoppingCartTable = _client?.GetOfflineTable<SyncShoppingCart>();

        if (syncShoppingCartTable is null) return false;

        SyncShoppingCart? itemToDelete = await syncShoppingCartTable.Where(w => w.Id == id)
                                                                    .ToAsyncEnumerable()
                                                                    .FirstOrDefaultAsync();

        if (itemToDelete is null) return false;

        await syncShoppingCartTable.DeleteItemAsync(itemToDelete);
        return true;
    }

    public async Task<SyncShoppingCart> GetShoppingCartAsync(string id)
    {
        await InitializeAsync();
        syncShoppingCartTable = _client?.GetOfflineTable<SyncShoppingCart>();

        if (syncShoppingCartTable is null) return new SyncShoppingCart();

        SyncShoppingCart? syncShoppingcart = await syncShoppingCartTable.GetItemAsync(id);

        return syncShoppingcart is not null ? syncShoppingcart : new SyncShoppingCart();
    }

    #endregion

    #region Get Data

    public async Task<List<SyncShoppingCart>> GetShoppingCartsAsync()
    {
        await InitializeAsync();
        syncShoppingCartTable = _client?.GetOfflineTable<SyncShoppingCart>();

        if (syncShoppingCartTable is null) return new List<SyncShoppingCart>();

        List<SyncShoppingCart> syncShoppingCarts = await syncShoppingCartTable.Where(w => w.Sent == false &&
                                                                                          w.StatusID == 10)
                                                                              .OrderByDescending(o => o.OrderDate)
                                                                              .IncludeTotalCount()
                                                                              .ToAsyncEnumerable()
                                                                              .ToListAsync();

        return syncShoppingCarts is not null ? syncShoppingCarts : new List<SyncShoppingCart>();
    }

    public async Task<int> GetShoppingCartsCountAsync()
    {
        await InitializeAsync();
        syncShoppingCartTable = _client?.GetOfflineTable<SyncShoppingCart>();

        if (syncShoppingCartTable is null) return 0;

        return await syncShoppingCartTable.Where(w => w.Sent == false &&
                                                      w.StatusID == 10)
                                          .ToAsyncEnumerable()
                                          .CountAsync();
    }

    #endregion

    #endregion

    #region ShoppingCartItems

    #region Base CRUD

    public async Task<string> SaveShoppingCartItemAsync(SyncShoppingCartItem upsertItem)
    {
        await InitializeAsync();
        syncShoppingCartItemTable = _client?.GetOfflineTable<SyncShoppingCartItem>();

        if (syncShoppingCartItemTable is null) return string.Empty;

        if (upsertItem.Id is null)
            await syncShoppingCartItemTable.InsertItemAsync(upsertItem);
        else
            await syncShoppingCartItemTable.ReplaceItemAsync(upsertItem);

        return upsertItem.Id is not null ? upsertItem.Id.ToString() : string.Empty;
    }

    public async Task<bool> DeleteShoppingCartItemAsync(string id)
    {
        await InitializeAsync();
        syncShoppingCartItemTable = _client?.GetOfflineTable<SyncShoppingCartItem>();

        if (syncShoppingCartItemTable is null) return false;

        SyncShoppingCartItem? itemToDelete = await syncShoppingCartItemTable.Where(w => w.Id == id)
                                                                            .ToAsyncEnumerable()
                                                                            .FirstOrDefaultAsync();

        if (itemToDelete is null) return false;

        await syncShoppingCartItemTable.DeleteItemAsync(itemToDelete);
        return true;
    }

    public async Task<SyncShoppingCartItem> GetShoppingCartItemAsync(string id)
    {
        await InitializeAsync();
        syncShoppingCartItemTable = _client?.GetOfflineTable<SyncShoppingCartItem>();

        if (syncShoppingCartItemTable is null) return new SyncShoppingCartItem();

        SyncShoppingCartItem? syncShoppingCartItem = await syncShoppingCartItemTable.GetItemAsync(id);

        return syncShoppingCartItem is not null ? syncShoppingCartItem : new SyncShoppingCartItem();
    }

    #endregion

    #region Get Data

    public virtual async Task<List<SyncShoppingCartItem>> GetShoppingCartItemsAsync(string id,
                                                                                    string searchParameter,
                                                                                    bool specialSort = false)
    {
        await InitializeAsync();
        syncShoppingCartItemTable = _client?.GetOfflineTable<SyncShoppingCartItem>();

        if (syncShoppingCartItemTable is null) return new List<SyncShoppingCartItem>();

        List<SyncShoppingCartItem> syncShoppingCartItems = await syncShoppingCartItemTable.Where(w => w.ShoppingCartID == id)
                                                                                          .IncludeTotalCount()
                                                                                          .ToAsyncEnumerable()
                                                                                          .ToListAsync();

        return syncShoppingCartItems is not null ? syncShoppingCartItems : new List<SyncShoppingCartItem>();
    }

    public virtual async Task<int> GetShoppingCartItemsCountAsync(string shoppingCartID)
    {
        await InitializeAsync();
        syncShoppingCartItemTable = _client?.GetOfflineTable<SyncShoppingCartItem>();

        if (syncShoppingCartItemTable is null) return 0;

        return await syncShoppingCartItemTable.Where(w => w.ShoppingCartID == shoppingCartID)
                                              .ToAsyncEnumerable()
                                              .CountAsync();
    }

    #endregion

    #endregion

    #region Orders

    #region Get Data

    public async Task<int> GetPendingOrdersCountAsync()
    {
        await InitializeAsync();
        syncShoppingCartTable = _client?.GetOfflineTable<SyncShoppingCart>();

        if (syncShoppingCartTable is null) return 0;

        return await syncShoppingCartTable.Where(w => w.Sent == false &&
                                                      w.StatusID < 10)
                                          .IncludeTotalCount()
                                          .ToAsyncEnumerable()
                                          .CountAsync();
    }

    #endregion

    #endregion

}

