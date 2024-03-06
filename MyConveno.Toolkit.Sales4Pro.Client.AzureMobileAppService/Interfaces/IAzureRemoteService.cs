namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService
{
    public interface IAzureRemoteService
    {
        string AzureURL { get; init; }

        Task InitializeAsync();

        Task<int> GetPendingOrdersCountAsync(string userName, DateTime fromDate);
        Task<List<SyncShoppingCart>> GetPendingOrdersAsync(bool userIsAdmin, string userName, DateTime fromDate);
        Task<List<SyncShoppingCart>> GetStagedOrdersAsync(bool userIsAdmin, string userName, DateTime fromDate);

        Task<SyncShoppingCart> GetOrderAsync(string id);
        Task<string> SaveOrderAsync(SyncShoppingCart upsertItem);
        Task<bool> DeleteOrderAsync(string id);

    }
}