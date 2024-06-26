﻿using Microsoft.Datasync.Client;

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

    #region Pending Orders

    public async Task<int> GetPendingOrdersCountAsync(bool userIsAdmin, string userName, DateTime fromDate)
    {
        await InitializeAsync();

        if (remoteShoppingCartTable is null) return 0;

        if (userIsAdmin)
        {
            return await remoteShoppingCartTable.Where(w => w.Status == 1 &&
                                                            w.OrderDate >= fromDate)
                                                .ToAsyncEnumerable()
                                                .CountAsync();
        }
        else
        {
            return await remoteShoppingCartTable.Where(w => w.Status == 1 &&
                                                            w.User == userName &&
                                                            w.OrderDate >= fromDate)
                                                .ToAsyncEnumerable()
                                                .CountAsync();
        }
    }

    public async Task<List<SyncShoppingCart>> GetPendingOrdersAsync(bool userIsAdmin, string userName, DateTime fromDate)
    {
        await InitializeAsync();

        List<SyncShoppingCart> syncShoppingCarts = new();

        if (remoteShoppingCartTable is null) return syncShoppingCarts;

        if (userIsAdmin)
        {
            syncShoppingCarts = await remoteShoppingCartTable.Where(w => w.Status == 1 &&
                                                                         w.OrderDate >= fromDate)
                                                             .IncludeTotalCount()
                                                             .ToAsyncEnumerable()
                                                             .ToListAsync();
        }
        else
        {
            syncShoppingCarts = await remoteShoppingCartTable.Where(w => w.Status == 1 &&
                                                                         w.OrderDate >= fromDate &&
                                                                         w.User == userName)
                                                             .IncludeTotalCount()
                                                             .ToAsyncEnumerable()
                                                             .ToListAsync();
        }

        return syncShoppingCarts is not null ? syncShoppingCarts : new List<SyncShoppingCart>();
    }

    public async Task<List<SyncShoppingCart>> GetStagedOrdersAsync(bool userIsAdmin, string userName, DateTime fromDate)
    {
        await InitializeAsync();

        List<SyncShoppingCart> syncShoppingCarts = new();

        if (remoteShoppingCartTable is null) return new List<SyncShoppingCart>();

        if (userIsAdmin)
        {
           syncShoppingCarts = await remoteShoppingCartTable.Where(w => w.Status <= -4 && 
                                                                   w.OrderDate >= fromDate)
                                                            .OrderByDescending(o => o.OrderDate)
                                                            .IncludeTotalCount()
                                                            .ToAsyncEnumerable()
                                                            .ToListAsync();
        }
        else
        {
            syncShoppingCarts = await remoteShoppingCartTable.Where(w => w.Status <= -4 &&
                                                                         w.OrderDate >= fromDate &&
                                                                         w.User == userName)
                                                             .OrderByDescending(o => o.OrderDate)
                                                             .IncludeTotalCount()
                                                             .ToAsyncEnumerable()
                                                             .ToListAsync();

        }
        return syncShoppingCarts is not null ? syncShoppingCarts : new List<SyncShoppingCart>();
    }

    #endregion

    #region One Order

    public async Task<SyncShoppingCart> GetOrderAsync(string id)
    {
        await InitializeAsync();

        if (remoteShoppingCartTable is null) return new SyncShoppingCart();

        SyncShoppingCart? syncShoppingcart = await remoteShoppingCartTable.GetItemAsync(id);

        return syncShoppingcart is not null ? syncShoppingcart : new SyncShoppingCart();
    }

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

}

