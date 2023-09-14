using Microsoft.Datasync.Client;
using Microsoft.Datasync.Client.Offline;
using Microsoft.Datasync.Client.Offline.Queue;
using Microsoft.Datasync.Client.SQLiteStore;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService;

public class AzureSyncService : IAzureSyncService
{
    private readonly DatasyncClient _client;

    private IOfflineTable<SyncCustomerFavorite>? syncCustomerFavoriteTable;
    private IOfflineTable<SyncCustomerNote>? syncCustomerNoteTable;
    private IOfflineTable<SyncShoppingCart>? syncShoppingCartTable;

    public event EventHandler<long?>? PendingOperationsChanged;

    public AzureSyncService(string url, OfflineSQLiteStore sqliteStore)
    {
        AzureURL = url;
        SQLiteStore = sqliteStore;


        var options = new DatasyncClientOptions
        {
            OfflineStore = SQLiteStore
        };

        // Create the datasync client.
        _client = TokenRequestor == null
            ? new DatasyncClient(AzureURL, options)
            : new DatasyncClient(AzureURL, new GenericAuthenticationProvider(TokenRequestor), options);
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
        long? pendingOperations = _client.PendingOperations == null ? 0 : _client.PendingOperations;
        PendingOperationsChanged?.Invoke(this, pendingOperations);
        // ****************************************************************************
    }

    #endregion

    #region Tasks

    public async Task InitializeAsync()
    {
        // Short circuit, in case we are already initialized.
        if (_initialized)
        { return; }

        // Wait to get the async initialization lock
        await _asyncLock.WaitAsync();

        //store.DefineTable<TodoItem>();
        SQLiteStore.DefineTable<SyncCustomerFavorite>();
        SQLiteStore.DefineTable<SyncCustomerNote>();
        SQLiteStore.DefineTable<SyncShoppingCart>();

        // Initialize the database
        await _client.InitializeOfflineStoreAsync();

        // Get a reference to the offline tables.
        syncCustomerFavoriteTable = _client.GetOfflineTable<SyncCustomerFavorite>();
        syncCustomerNoteTable = _client.GetOfflineTable<SyncCustomerNote>();
        syncShoppingCartTable = _client.GetOfflineTable<SyncShoppingCart>();

        // Set _initialized to true to prevent duplication of locking.
        _initialized = true;

        _asyncLock.Release();
    }

    public async Task PurgeAllTables()
    {
        await InitializeAsync();

        if (syncCustomerNoteTable is not null)
            await syncCustomerNoteTable.PurgeItemsAsync("", new PurgeOptions { DiscardPendingOperations = true });

        if (syncCustomerFavoriteTable is not null)
            await syncCustomerFavoriteTable.PurgeItemsAsync("", new PurgeOptions { DiscardPendingOperations = true });

        if (syncShoppingCartTable is not null)
            await syncShoppingCartTable.PurgeItemsAsync("", new PurgeOptions { DiscardPendingOperations = true });

        UpdatePendingOperationDisplay();
    }

    public async Task<bool> Synchronize(string userName, bool pullTables)
    {
        bool success = false;

        UpdatePendingOperationDisplay();

        if (_syncIsRunning)
        {
            return success;  // false
        }
        _syncIsRunning = true;

        ReadOnlyCollection<TableOperationError>? syncErrors = null;

        try
        {
            await InitializeAsync();


            if (syncCustomerNoteTable is not null)
                await syncCustomerNoteTable.PushItemsAsync();

            if (syncCustomerFavoriteTable is not null)
                await syncCustomerFavoriteTable.PushItemsAsync();

            if (syncShoppingCartTable is not null)
                await syncShoppingCartTable.PushItemsAsync();



            if (pullTables && (string.IsNullOrEmpty(userName) == false))
            {
                if (syncCustomerNoteTable is not null)
                    await syncCustomerNoteTable.PullItemsAsync();

                if (syncCustomerFavoriteTable is not null)
                    await syncCustomerFavoriteTable.PullItemsAsync(syncCustomerFavoriteTable.CreateQuery().Where(w => w.UserName == userName));

                if (syncShoppingCartTable is not null)
                    await syncShoppingCartTable.PullItemsAsync(syncShoppingCartTable.CreateQuery().Where(w => w.User == userName && w.Status == 10));
            }

            success = true;
        }
        catch (PushFailedException exc)
        {
            if (exc?.PushResult != null)
            {
                syncErrors = (ReadOnlyCollection<TableOperationError>?)exc.PushResult.Errors;
            }
        }

        if (syncErrors != null)
        {
            foreach (var error in syncErrors)
            {
                if (error.OperationKind == TableOperationKind.Update && error.Result != null)
                {
                    //Update failed, reverting to server's copy.
                    await error.CancelAndUpdateItemAsync(error.Result);
                }
                else
                {
                    // Discard local change.
                    await error.CancelAndDiscardItemAsync();
                }

                Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);

                success = false;
            }
        }

        UpdatePendingOperationDisplay();
        _syncIsRunning = false;
        return success;
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

        UpdatePendingOperationDisplay();

        return upsertItem.Id is not null ? upsertItem.Id.ToString() : string.Empty;
    }

    public async Task<bool> DeleteCustomerFavoriteAsync(string userName, string customerNumber)
    {
        await InitializeAsync();
        syncCustomerFavoriteTable = _client?.GetOfflineTable<SyncCustomerFavorite>();

        if (syncCustomerFavoriteTable is null) return false;

        SyncCustomerFavorite? itemToDelete = await syncCustomerFavoriteTable.Where(w => w.UserName == userName &&
                                                                                        w.CustomerNumber == customerNumber)
                                                                            .ToAsyncEnumerable()
                                                                            .FirstOrDefaultAsync();

        if (itemToDelete is null) return false;

        await syncCustomerFavoriteTable.DeleteItemAsync(itemToDelete);

        UpdatePendingOperationDisplay();

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

    public async Task<List<SyncCustomerFavorite>> GetCustomerFavoritesAsync(string userName)
    {
        await InitializeAsync();

        syncCustomerFavoriteTable = _client?.GetOfflineTable<SyncCustomerFavorite>();

        if (syncCustomerFavoriteTable is null) return new List<SyncCustomerFavorite>();

        List<SyncCustomerFavorite> syncCustomerFavorites = await syncCustomerFavoriteTable.Where(w => w.UserName == userName)
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

        UpdatePendingOperationDisplay();

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

        UpdatePendingOperationDisplay();

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

    #region Get Data

    public async Task<List<SyncShoppingCart>> GetShoppingCartsAsync()
    {
        await InitializeAsync();

        if (syncShoppingCartTable is null) return new List<SyncShoppingCart>();

        List<SyncShoppingCart> syncShoppingCarts = await syncShoppingCartTable.Where(w => w.SentDateTime <= new DateTime(2000, 1, 1) &&
                                                                                          w.Status == 10)
                                                                              .OrderByDescending(o => o.OrderDate)
                                                                              .IncludeTotalCount()
                                                                              .ToAsyncEnumerable()
                                                                              .ToListAsync();

        return syncShoppingCarts is not null ? syncShoppingCarts : new List<SyncShoppingCart>();
    }

    public async Task<SyncShoppingCart> GetShoppingCartAsync(string id)
    {
        await InitializeAsync();

        if (syncShoppingCartTable is null) return new SyncShoppingCart();

        SyncShoppingCart? syncShoppingcart = await syncShoppingCartTable.GetItemAsync(id);

        return syncShoppingcart is not null ? syncShoppingcart : new SyncShoppingCart();
    }

    public async Task<int> GetShoppingCartsCountAsync()
    {
        await InitializeAsync();

        if (syncShoppingCartTable is null) return 0;

        return await syncShoppingCartTable.Where(w => w.SentDateTime <= new DateTime(2000, 1, 1) &&
                                                      w.Status == 10)
                                          .ToAsyncEnumerable()
                                          .CountAsync();
    }

    #endregion

    #region Base CRUD

    public async Task<string> SaveShoppingCartAsync(SyncShoppingCart upsertItem)
    {
        await InitializeAsync();

        if (syncShoppingCartTable is null) return string.Empty;

        if (upsertItem.Id is null)
            await syncShoppingCartTable.InsertItemAsync(upsertItem);
        else
            await syncShoppingCartTable.ReplaceItemAsync(upsertItem);

        UpdatePendingOperationDisplay();

        return upsertItem.Id is not null ? upsertItem.Id.ToString() : string.Empty;
    }

    public async Task<bool> DeleteShoppingCartAsync(string id)
    {
        await InitializeAsync();

        if (syncShoppingCartTable is null) return false;

        SyncShoppingCart? itemToDelete = await syncShoppingCartTable.Where(w => w.Id == id)
                                                                    .ToAsyncEnumerable()
                                                                    .FirstOrDefaultAsync();

        if (itemToDelete is null) return false;

        await syncShoppingCartTable.DeleteItemAsync(itemToDelete);

        UpdatePendingOperationDisplay();

        return true;
    }

    #endregion

    #endregion

    #region Orders

    #region Get Data

    public async Task<int> GetPendingOrdersCountAsync()
    {
        await InitializeAsync();

        if (syncShoppingCartTable is null) return 0;

        return await syncShoppingCartTable.Where(w => w.Status == 1)
                                          .IncludeTotalCount()
                                          .ToAsyncEnumerable()
                                          .CountAsync();
    }

    #endregion

    #endregion

}

