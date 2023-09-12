using Microsoft.Datasync.Client;

namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService;

public class AzureRemoteService : IAzureRemoteService
{
    private readonly DatasyncClient _client;

    private IRemoteTable<SyncShoppingCart>? remoteShoppingCartTable;

    public AzureRemoteService(string url)
    {
        AzureURL = url;

        var options = new DatasyncClientOptions
        { };

        // Create the datasync client.
        _client = TokenRequestor == null
            ? new DatasyncClient(AzureURL, options)
            : new DatasyncClient(AzureURL, new GenericAuthenticationProvider(TokenRequestor), options);
    }

    /// <summary>
    /// When set to true, the client and table and both initialized.
    /// </summary>
    private bool _initialized = false;

    /// <summary>
    /// Used for locking the initialization block to ensure only one initialization happens.
    /// </summary>
    private readonly SemaphoreSlim _asyncLock = new(1, 1);

    /// <summary>
    /// When using authentication, the token requestor to use.
    /// </summary>
    public Func<Task<AuthenticationToken>>? TokenRequestor;

    public string AzureURL { get; init; }

    #region Commands

    #endregion

    #region Tasks

    public async Task InitializeAsync()
    {
        // Short circuit, in case we are already initialized.
        if (_initialized)
        { return; }

        // Wait to get the async initialization lock
        await _asyncLock.WaitAsync();

        // Get a reference to the remote tables.
        remoteShoppingCartTable = _client.GetRemoteTable<SyncShoppingCart>();

        // Set _initialized to true to prevent duplication of locking.
        _initialized = true;

        _asyncLock.Release();
    }

    #endregion

    #region Orders

    #region Get Data

    public async Task<List<SyncShoppingCart>> GetOrdersAsync()
    {
        await InitializeAsync();

        if (remoteShoppingCartTable is null) return new List<SyncShoppingCart>();

        List<SyncShoppingCart> syncShoppingCarts = await remoteShoppingCartTable.Where(w => w.Status < 10)
                                                                                .OrderByDescending(o => o.OrderDate)
                                                                                .IncludeTotalCount()
                                                                                .ToAsyncEnumerable()
                                                                                .ToListAsync();

        return syncShoppingCarts is not null ? syncShoppingCarts : new List<SyncShoppingCart>();
    }

    public async Task<int> GetOrdersCountAsync()
    {
        await InitializeAsync();

        if (remoteShoppingCartTable is null) return 0;

        return await remoteShoppingCartTable.Where(w => w.Status < 10)
                                            .ToAsyncEnumerable()
                                            .CountAsync();
    }

    #endregion

    #region Base CRUD

    public async Task<string> SaveOrderAsync(SyncShoppingCart upsertItem)
    {
        await InitializeAsync();

        if (remoteShoppingCartTable is null) return string.Empty;

        if (upsertItem.Id is null)
            await remoteShoppingCartTable.InsertItemAsync(upsertItem);
        else
            await remoteShoppingCartTable.ReplaceItemAsync(upsertItem);

        return upsertItem.Id is not null ? upsertItem.Id.ToString() : string.Empty;
    }

    public async Task<bool> DeleteOrderAsync(string id)
    {
        await InitializeAsync();

        if (remoteShoppingCartTable is null) return false;

        SyncShoppingCart? itemToDelete = await remoteShoppingCartTable.Where(w => w.Id == id)
                                                                    .ToAsyncEnumerable()
                                                                    .FirstOrDefaultAsync();

        if (itemToDelete is null) return false;

        await remoteShoppingCartTable.DeleteItemAsync(itemToDelete);

        return true;
    }

    #endregion

    #endregion

}

