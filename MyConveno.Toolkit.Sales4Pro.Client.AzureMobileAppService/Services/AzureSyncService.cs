using Microsoft.Datasync.Client;
using Microsoft.Datasync.Client.Offline;
using Microsoft.Datasync.Client.Offline.Queue;
using Microsoft.Datasync.Client.SQLiteStore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService;

public class AzureSyncService : IAzureSyncService
{
    private DatasyncClient _client;

    private IOfflineTable<SyncCustomerFavorite> syncCustomerFavoriteTable;
    private IOfflineTable<SyncCustomerNote> syncCustomerNoteTable;
    private IOfflineTable<SyncShoppingCart> syncShoppingCartTable;
    private IOfflineTable<SyncShoppingCartItem> syncShoppingCartItemTable;

    public AzureSyncService(string url, string paraLocalSyncDBName)
    {
        AzureURL = url;
        LocalSyncDBName = paraLocalSyncDBName;
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
    public Func<Task<AuthenticationToken>> TokenRequestor;

    public string AzureURL { get; set; }
    public string LocalSyncDBName { get; set; }

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

    //public async Task InitializeAsync()
    //{
    //    if (_client != null)
    //        return;

    //    MobileServiceSQLiteStore store = new(localSyncDBName);

    //    store.DefineTable<SyncCustomerFavorite>();
    //    store.DefineTable<SyncCustomerNote>();
    //    store.DefineTable<SyncShoppingCart>();
    //    store.DefineTable<SyncShoppingCartItem>();

    //    _client = new MobileServiceClient(AzureURL);
    //    _client.EventManager.Subscribe<StoreOperationCompletedEvent>(StoreOperationEventHandlerAsync);
    //    await _client.SyncContext.InitializeAsync(store, StoreTrackingOptions.NotifyLocalAndServerOperations);

    //    syncCustomerFavoriteTable = _client.GetSyncTable<SyncCustomerFavorite>();
    //    syncCustomerNoteTable = _client.GetSyncTable<SyncCustomerNote>();
    //    syncShoppingCartTable = _client.GetSyncTable<SyncShoppingCart>();
    //    syncShoppingCartItemTable = _client.GetSyncTable<SyncShoppingCartItem>();
    //}



    //private async Task InitializeAsync()
    //{
    //    // Short circuit, in case we are already initialized.
    //    if (_initialized)
    //    {
    //        return;
    //    }

    //    try
    //    {
    //        // Wait to get the async initialization lock
    //        await _asyncLock.WaitAsync();
    //        if (_initialized)
    //        {
    //            // This will also execute the async lock.
    //            return;
    //        }

    //        var options = new DatasyncClientOptions
    //        {
    //            HttpPipeline = new HttpMessageHandler[] { new LoggingHandler() }
    //        };

    //        // Initialize the client.
    //        _client = TokenRequestor == null
    //            ? new DatasyncClient(Constants.ServiceUri, options)
    //            : new DatasyncClient(Constants.ServiceUri, new GenericAuthenticationProvider(TokenRequestor), options);
    //        _table = _client.GetRemoteTable<TodoItem>();

    //        // Set _initialied to true to prevent duplication of locking.
    //        _initialized = true;
    //    }
    //    catch (Exception)
    //    {
    //        // Re-throw the exception.
    //        throw;
    //    }
    //    finally
    //    {
    //        _asyncLock.Release();
    //    }
    //}




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
        var connectionString = new UriBuilder { Scheme = "file", Path = LocalSyncDBName, Query = "?mode=rwc" }.Uri.ToString();
        var store = new OfflineSQLiteStore(connectionString);

        //store.DefineTable<TodoItem>();
        store.DefineTable<SyncCustomerFavorite>();
        store.DefineTable<SyncCustomerNote>();
        store.DefineTable<SyncShoppingCart>();
        store.DefineTable<SyncShoppingCartItem>();

        var options = new DatasyncClientOptions
        {
            OfflineStore = store
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

    public async Task<bool> Synchronize(string userid, bool pullTables = true)
    {
        bool success = await SyncAllTablesAsync(userid, pullTables);

        UpdatePendingOperationDisplay();

        return success;
    }

    public async Task<bool> SyncAllTablesAsync(string userId, bool pullTables)
    {
        ReadOnlyCollection<TableOperationError> syncErrors = null;

        try
        {
            await InitializeAsync();

            //////await Task.Delay(1000);

            await syncCustomerNoteTable.PushItemsAsync();
            await syncCustomerFavoriteTable.PushItemsAsync();
            await syncShoppingCartTable.PushItemsAsync();
            await syncShoppingCartItemTable.PushItemsAsync();

            //await _client.SyncContext.PushAsync();

            //////await Task.Delay(1000);

            if (pullTables && (string.IsNullOrEmpty(userId) == false))
            {
                PullOptions pullOptions = new() { };

                await syncCustomerNoteTable.PullItemsAsync();
                await syncCustomerFavoriteTable.PullItemsAsync();
                await syncShoppingCartTable.PullItemsAsync();
                await syncShoppingCartItemTable.PullItemsAsync();



                //await syncCustomerNoteTable.PullItemsAsync();
                //await syncCustomerFavoriteTable.PullItemsAsync(syncCustomerFavoriteTable.CreateQuery().Where(w => w.UserID == userId));
                //await syncShoppingCartTable.PullItemsAsync(syncShoppingCartTable.CreateQuery().Where(w => w.UserID == userId && w.StatusID == 10));
                //await syncShoppingCartItemTable.PullItemsAsync(syncShoppingCartItemTable.CreateQuery().Where(w => w.UserID == userId));
            }
            else
            {
                return false;
            }
            UpdatePendingOperationDisplay();
        }
        catch (PushFailedException exc)
        {
            if (exc?.PushResult != null)
            { syncErrors = (ReadOnlyCollection<TableOperationError>?)exc?.PushResult.Errors; }
        }
        return true;
    }

    //////////private async void StoreOperationEventHandlerAsync(StoreOperationCompletedEvent mobileServiceEvent)
    //////////{
    //////////    StoreOperation so = mobileServiceEvent.Operation;
    //////////    await Task.Delay(100);

    //////////    UpdatePendingOperationDisplay();
    //////////}

    #endregion

    #region CustomerFavorites

    public async Task<string> SaveCustomerFavoriteAsync(SyncCustomerFavorite syncCustomerFavorite)
    {
        await InitializeAsync();
        syncCustomerFavoriteTable = _client.GetOfflineTable<SyncCustomerFavorite>();

        if (syncCustomerFavoriteTable is not null)
        {
            if (syncCustomerFavorite.Id == null)
                await syncCustomerFavoriteTable.InsertItemAsync(syncCustomerFavorite);
            else
                await syncCustomerFavoriteTable.RefreshItemAsync(syncCustomerFavorite);

            return syncCustomerFavorite.Id is not null ? syncCustomerFavorite.Id.ToString() : string.Empty;
        }
        else
            return string.Empty;
    }

    public async Task<bool> DeleteCustomerFavoriteAsync(string userId, string customerNumber)
    {
        await InitializeAsync();
        syncCustomerFavoriteTable = _client.GetOfflineTable<SyncCustomerFavorite>();

        if (syncCustomerFavoriteTable is not null)
        {
            SyncCustomerFavorite syncCustomerFavorite = syncCustomerFavoriteTable.Where(w => w.UserID == userId &&
                                                                                             w.CustomerNumber == customerNumber)
                                                                                            .Query
                                                                                            .FirstOrDefault();

            if (syncCustomerFavorite != null)
                await syncCustomerFavoriteTable.DeleteItemAsync(syncCustomerFavorite);

            return true;
        }
        else
            return false;
    }

    public async Task<List<SyncCustomerFavorite>> GetCustomerFavoritesAsync(string userId)
    {
        await InitializeAsync();

        syncCustomerFavoriteTable = _client.GetOfflineTable<SyncCustomerFavorite>();

        //var syncCustomerFavorites2 = syncCustomerFavoriteTable.GetAsyncItems().ToListAsync();
        //var syncCustomerFavorites3 = syncCustomerFavoriteTable.Where(w => w.UserID == userId).ToListAsync();

        IEnumerable<SyncCustomerFavorite> syncCustomerFavorites = syncCustomerFavoriteTable.Where(w => w.UserID == userId).Query;
        return syncCustomerFavorites.ToList();
    }

    public async Task<bool> ComputeCustomerIsFavoriteAsync(string userID, string customerNumber)
    {
        await InitializeAsync();
        syncCustomerFavoriteTable = _client.GetOfflineTable<SyncCustomerFavorite>();

        IQueryable<SyncCustomerFavorite> syncCustomerFavorites = (from scn in syncCustomerFavoriteTable
                                                                  where scn.UserID == userID &&
                                                                        scn.CustomerNumber == customerNumber
                                                                  select scn).Query;
        return syncCustomerFavorites.ToList().Count > 0;
    }

    #endregion

    #region CustomerNotes

    public async Task<string> SaveCustomerNoteAsync(SyncCustomerNote syncCustomerNote)
    {
        await InitializeAsync();
        syncCustomerNoteTable = _client.GetOfflineTable<SyncCustomerNote>();

        //SyncCustomerNote syncCustomerNote = new SyncCustomerNote();
        //syncCustomerNote.SerializeMetadata(customerNote);

        if (syncCustomerNote.Id == null)
            await syncCustomerNoteTable.InsertItemAsync(syncCustomerNote);
        else
            await syncCustomerNoteTable.RefreshItemAsync(syncCustomerNote);

        return syncCustomerNote.Id.ToString();
    }

    public async Task DeleteCustomerNoteAsync(SyncCustomerNote syncCustomerNote)
    {
        await InitializeAsync();
        syncCustomerNoteTable = _client.GetOfflineTable<SyncCustomerNote>();

        //SyncCustomerNote syncCustomerNote = new SyncCustomerNote();
        //syncCustomerNote.SerializeMetadata(customerNote);

        await syncCustomerNoteTable.DeleteItemAsync(syncCustomerNote);
    }

    public async Task<List<SyncCustomerNote>> GetCustomerNotesAsync(string customerNumber)
    {
        await InitializeAsync();
        syncCustomerNoteTable = _client.GetOfflineTable<SyncCustomerNote>();

        IQueryable<SyncCustomerNote> syncCustomerNotes = (from scn in syncCustomerNoteTable
                                                          where scn.CustomerNumber == customerNumber
                                                          orderby scn.NoteCreated
                                                          select scn).Query;

        //List<SyncCustomerNote> customerNotes = new List<SyncCustomerNote>();
        //foreach (SyncCustomerNote item in syncCustomerNotes)
        //{
        //    customerNotes.Add(item);
        //}

        ////List<CustomerNote> cust = await localSyncDataDBConnection.Table<CustomerNote>().Where(c => c.CustomerID == CustomerID).OrderBy(c => c.Created).ToListAsync();
        //return customerNotes;

        return syncCustomerNotes.ToList();
    }

    #endregion

    #region ShoppingCarts

    /// <summary>
    /// ShoppingCart speichern
    /// </summary>
    /// <param name="shoppingCart"></param>
    /// <returns></returns>
    public async Task<string> SaveShoppingCartAsync(SyncShoppingCart syncShoppingCart)
    {
        // Erstelle alle Zeiger auf die lokalen Synctabellen
        await InitializeAsync();

        // Hole eine Refernz auf die SyncShoppingCartTable
        syncShoppingCartTable = _client.GetOfflineTable<SyncShoppingCart>();

        // Serialisiere den Paramater in das neue SyncShoppingCart-Objekt
        // 29.12.2022 MC >>  syncShoppingCart.SerializeMetadata();

        if (syncShoppingCart.Id == null) // Es handelt sich um eine neue ShoppingCart
            await syncShoppingCartTable.InsertItemAsync(syncShoppingCart);
        else  // Es handelt sich um eine bestehende ShoppingCart (Id ist da)
            await syncShoppingCartTable.RefreshItemAsync(syncShoppingCart);

        // gib die Id zurück. Bei einer neuen ShoppingCart wird diese ID gespeichert  
        return syncShoppingCart.Id.ToString();
    }

    /// <summary>
    /// ShoppingCart löschen
    /// </summary>
    /// <param name="shoppingCart"></param>
    /// <returns></returns>
    public async Task<bool> DeleteShoppingCartAsync(SyncShoppingCart syncShoppingCart)
    {
        // Erstelle alle Zeiger auf die lokalen Synctabellen
        await InitializeAsync();

        // Hole eine Refernz auf die SyncShoppingCartTable
        syncShoppingCartTable = _client.GetOfflineTable<SyncShoppingCart>();

        //// Erzeuge ein neues SyncShoppingCart-Objekt
        //SyncShoppingCart syncShoppingCart = new SyncShoppingCart();

        // Serialisiere den Paramater in das neue SyncShoppingCart-Objekt
        // 29.12.2022 MC >>   syncShoppingCart.SerializeMetadata();


        // Lösche die übergebene ShoppingCart (soft delete)
        await syncShoppingCartTable.DeleteItemAsync(syncShoppingCart);
        // und gebe true zurück, wenn OK
        return true;
    }

    /// <summary>
    /// Hole ShoppingCarts
    /// </summary>
    /// <param name="filterList"></param>
    /// <returns></returns>
    public async Task<List<SyncShoppingCart>> GetShoppingCartsAsync()
    {
        await InitializeAsync();
        syncShoppingCartTable = _client.GetOfflineTable<SyncShoppingCart>();

        IQueryable<SyncShoppingCart> syncShoppingCarts = syncShoppingCartTable.Where(w => w.Sent == false &&
                                                                                          w.StatusID == 10)
                                                                              .OrderByDescending(o => o.OrderDate)
                                                                              .IncludeTotalCount()
                                                                              .Query;

        return syncShoppingCarts.ToList();
    }


    /// <summary>
    /// Hole ShoppingCarts (mit Berücksichtigung der übergebenen Filterliste)
    /// </summary>
    /// <param name="filterList"></param>
    /// <returns></returns>
    //public async Task<List<SyncShoppingCart>> GetDeserializedShoppingCartsAsync(List<ShoppingCartsFilterEntryItem> filterList = null)
    //{
    //    await InitializeAsync();
    //    syncShoppingCartTable = _client.GetOfflineTable<SyncShoppingCart>();


    //    List<SyncShoppingCart> syncShoppingCarts = await (from sc in syncShoppingCartTable
    //                                                      where sc.Sent == false
    //                                                      orderby sc.OrderDate descending
    //                                                      select sc).ToListAsync();

    //    foreach (SyncShoppingCart item in syncShoppingCarts)
    //    {
    //        item.DeserializeMetadata();
    //    }

    //    if (filterList != null)
    //    {
    //        foreach (ShoppingCartsFilterEntryItem filter in filterList)
    //        {
    //            switch (filter.FilterGroup)
    //            {
    //                case ShoppingCartsFilterGroupesEnum.Text:
    //                    syncShoppingCarts = syncShoppingCarts.Where(s => s.Customer.Name1.ToLower().Contains(filter.FilterTextContent.ToLower())
    //                    || s.Customer.Name2.ToLower().Contains(filter.FilterTextContent.ToLower())
    //                    || s.Customer.Name3.ToLower().Contains(filter.FilterTextContent.ToLower())
    //                    || s.Customer.Street.ToLower().Contains(filter.FilterTextContent.ToLower())
    //                    || s.Customer.CustomerNumber.ToLower().Contains(filter.FilterTextContent.ToLower())
    //                    || s.Customer.City.ToLower().Contains(filter.FilterTextContent.ToLower())
    //                    || s.Customer.ZIP.ToLower().Contains(filter.FilterTextContent.ToLower())
    //                    || s.Customer.CountryName.ToLower().Contains(filter.FilterTextContent.ToLower())
    //                    || s.Header.CustomerOrderNumber.ToLower().Contains(filter.FilterTextContent.ToLower())
    //                    || s.OrderNumber.ToLower().Contains(filter.FilterTextContent.ToLower())).ToList();
    //                    break;
    //                //case SyncFilterGroupesEnum.Season:
    //                //    shoppingcarts = shoppingcarts.Where(s => s.Season.SeasonID == filter.FilterTextContent).ToList();
    //                //    break;
    //                case ShoppingCartsFilterGroupesEnum.OrderType:
    //                    syncShoppingCarts = syncShoppingCarts.Where(s => s.OrderTypeNumber == filter.FilterTextContent).ToList();
    //                    break;
    //                case ShoppingCartsFilterGroupesEnum.PreFilter:
    //                    syncShoppingCarts = syncShoppingCarts.Where(s => s.OrderDate > filter.FilterDateTimeContent).ToList();
    //                    break;
    //                case ShoppingCartsFilterGroupesEnum.OrderDate:
    //                    DateTime begin = filter.FilterDateTimeContent.Date;
    //                    DateTime end = filter.FilterDateTimeEndContent.Date.AddDays(1);
    //                    syncShoppingCarts = syncShoppingCarts.Where(s => s.OrderDate >= begin && s.OrderDate < end).ToList();
    //                    break;
    //            }
    //        }
    //    }

    //    return syncShoppingCarts;
    //}

    public async Task<SyncShoppingCart> GetShoppingCartAsync(string shoppingCartID)
    {
        await InitializeAsync();
        syncShoppingCartTable = _client.GetOfflineTable<SyncShoppingCart>();


        SyncShoppingCart syncShoppingcart = await syncShoppingCartTable.GetItemAsync(shoppingCartID);
        if (syncShoppingcart != null)
        {
            // 29.12.2022 MC >>  syncShoppingcart.DeserializeMetadata();
            return syncShoppingcart;
        }
        else
            return null;
    }

    public async Task<int> GetShoppingCartCountAsync()
    {
        await InitializeAsync();
        syncShoppingCartTable = _client.GetOfflineTable<SyncShoppingCart>();

        IQueryable<SyncShoppingCart> syncShoppingCarts = syncShoppingCartTable.Where(w => w.Sent == false &&
                                                                                          w.StatusID == 10)
                                                                              .IncludeTotalCount()
                                                                              .Query;

        return syncShoppingCarts.Count();
    }

    public async Task ResetShoppingCartPricesAsync(string ShoppingCartID)
    {
        await InitializeAsync();
        syncShoppingCartTable = _client.GetOfflineTable<SyncShoppingCart>();


        SyncShoppingCart cart = await GetShoppingCartAsync(ShoppingCartID);
        // 29.12.2022 MC >>   List<SyncShoppingCartItem> items = await GetShoppingCartItemsAsync(ShoppingCartID, "");

        //foreach (ShoppingCartItem item in items)
        //{
        //    // MC 23.01.2020
        //    PricePricelist_Join price = await DataAccessPricesInstance.GetColorPriceAsync(item.ColorID, item.SizerunID, cart.PricelistID, cart.CustomerCountryCode);
        //    if (price != null)
        //    {
        //        item.PasteData(price);
        //    }
        //    else
        //    {
        //        item.PasteData(new PricePricelist_Join());
        //    }
        //}

        //await sales4ProDatabaseConnection.UpdateAllAsyncRetry(items);
    }

    // 29.12.2022 MC >>
    //public string GetExportFileName(SyncShoppingCart cart, int itemsCount)
    //{
    //    return DateTime.Now.ToString("yyyyMMddmmss") + "_Auftrag-" + cart.OrderNumber + "_Kunde-" + cart.Customer.CustomerNumber;
    //}

    #endregion

    #region ShoppingCartItems

    public virtual async Task<List<SyncShoppingCartItem>> GetShoppingCartItemsAsync(string shoppingCartID, string searchParameter, bool specialSort = false)
    {
        await InitializeAsync();
        syncShoppingCartItemTable = _client.GetOfflineTable<SyncShoppingCartItem>();

        try
        {
            IQueryable<SyncShoppingCartItem> syncShoppingCartItems = (from sci in syncShoppingCartItemTable
                                                                      where sci.ShoppingCartID == shoppingCartID
                                                                      select sci).Query;

            foreach (SyncShoppingCartItem item in syncShoppingCartItems)
            {
                // 29.12.2022 MC >>  item.DeserializeMetadata();
            }

            //if (searchParameter != string.Empty)
            //{
            //    syncShoppingCartItems = syncShoppingCartItems.Where(f => f.Article().ArticleNumber.StartsWith(searchParameter)).ToList();
            //}

            int pp = 1;
            foreach (SyncShoppingCartItem i in syncShoppingCartItems)
            {
                pp++;
            }
            return syncShoppingCartItems.ToList();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public virtual async Task<int> GetShoppingCartItemsCountAsync(string shoppingCartID)
    {
        await InitializeAsync();
        syncShoppingCartItemTable = _client.GetOfflineTable<SyncShoppingCartItem>();

        IQueryable<SyncShoppingCartItem> syncShoppingCartItems = syncShoppingCartItemTable.Where(w => w.ShoppingCartID == shoppingCartID)
                                                                                          .IncludeTotalCount()
                                                                                          .Query;
        return syncShoppingCartItems.Count();
    }

    public virtual async Task<MemoryStream> GetShoppingCartItemCatalogPDFAsync()
    {
        await InitializeAsync();
        syncShoppingCartItemTable = _client.GetOfflineTable<SyncShoppingCartItem>();

        //ShoppingCartItemCatalog confirm = new ShoppingCartItemCatalog();

        //if (string.IsNullOrEmpty(pricelistID))
        //{
        //    pricelistID = (await GetDefaultPricelistAsync("")).PricelistID;
        //}

        ////List<ShoppingCartItem> items = await GetCatalogItemsAsync(filterList, new List<IRuleEntries>(), false, (await GetDefaultPricelistAsync(PricelistID)).PricelistID, true);
        ////Client client = await BasicDatabaseService.GetBasicDatabaseServiceInstance().GetCurrentClientAsync();

        ////MemoryStream pdfStream = await confirm.GenerateAsync(items, client, await GetCurrentUserAsync());
        ////return pdfStream;

        return null;
    }

    public async Task<string> SaveShoppingCartItemAsync(SyncShoppingCartItem syncShoppingCartItem)
    {
        // Erstelle alle Zeiger auf die lokalen Synctabellen
        await InitializeAsync();

        // Hole eine Refernz auf die SyncShoppingCartItemTable
        syncShoppingCartItemTable = _client.GetOfflineTable<SyncShoppingCartItem>();

        // Serialisiere den Paramater in das neue SyncShoppingCart-Objekt
        // 29.12.2022 MC >> syncShoppingCartItem.SerializeMetadata();


        if (syncShoppingCartItem.Id == null)
            await syncShoppingCartItemTable.InsertItemAsync(syncShoppingCartItem);
        else
            await syncShoppingCartItemTable.RefreshItemAsync(syncShoppingCartItem);

        return syncShoppingCartItem.Id.ToString();
    }

    public async Task<bool> DeleteShoppingCartItemAsync(SyncShoppingCartItem syncShoppingCartItem)
    {
        // Erstelle alle Zeiger auf die lokalen Synctabellen
        await InitializeAsync();

        // Hole eine Refernz auf die SyncShoppingCartItemTable
        syncShoppingCartItemTable = _client.GetOfflineTable<SyncShoppingCartItem>();

        // Serialisiere den Paramater in das neue SyncShoppingCart-Objekt
        // 29.12.2022 MC >> syncShoppingCartItem.SerializeMetadata();

        // Lösche die übergebene ShoppingCart (soft delete)
        await syncShoppingCartItemTable.DeleteItemAsync(syncShoppingCartItem);
        // und gebe true zurück, wenn OK
        return true;
    }

    #endregion

    #region Orders

    public virtual async Task<int> GetPendingOrdersCountAsync()
    {
        await InitializeAsync();
        syncShoppingCartTable = _client.GetOfflineTable<SyncShoppingCart>();

        IQueryable<SyncShoppingCart> syncShoppingCarts = syncShoppingCartTable.Where(w => w.Sent == false &&
                                                                                          w.StatusID < 10)
                                                                              .IncludeTotalCount()
                                                                              .Query;

        return syncShoppingCarts.Count();
    }

    #endregion

}

